using LEFiles.Core.Helpers;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Models.Configuration;
using LEFiles.Models.Entities;
using LEFiles.Services.Contracts.Authentication;
using LEFiles.Services.ServiceModels.Authentication.Request;
using LEFiles.Services.ServiceModels.Authentication.Responses;
using LEFiles.Services.ServiceModels.Clients.Requests;
using LEFiles.Services.ServiceModels.Clients.Responses;
using LEFiles.Services.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.Service.Authentication
{
  public class BasicAuthenticationService : IAuthenticationService
  {
    private readonly AppDbContext _context;
    private readonly List<JWTConfig> _jwtConfig;

    public BasicAuthenticationService(AppDbContext context, List<JWTConfig> jwtConfig)
    {
      _context = context;
      _jwtConfig = jwtConfig;
    }

    public IDataResult<ClientTokenResponse> ClientLogin(GetClientTokenRequest clientTokenRequest)
    {
      var client = _context.Clients.SingleOrDefault(x => x.ClientSecret == clientTokenRequest.Secret && x.HarddiskSerialNumber == clientTokenRequest.HarddiskSN.Trim());
      if (client == null)
      {
        throw new HttpRequestException("", null, HttpStatusCode.NotFound);
      }
      var clientId = client.ClientId;
      var clientName = client.ClientName;
      var clientConfig = _jwtConfig.SingleOrDefault(x => x.TokenName == "ClientBearer");
      if (clientConfig == null) {
        throw new HttpRequestException("Client Jwt Configuration not found", null, HttpStatusCode.NotFound);
      }
      var expiration = DateTime.UtcNow.AddHours(8);
      var jwtCreator = new JWTCreator(clientConfig, client.ClientId, new string[1] { "Client" }, expiration, new Dictionary<string, string> { { "iss", "LEFiles" } });
      var token = jwtCreator.GenerateToken();
      var response = new ClientTokenResponse
      {
        Expiration = expiration,
        Token = token
      };
      return new SuccessDataResult<ClientTokenResponse>(response);
    }

    public IDataResult<UserLoginResponse> Login(UserLoginRequest loginRequest)
    {
      var username = loginRequest.Username;
      var password = loginRequest.Password;
      var userItem = _context.Users.SingleOrDefault(x => x.Username.ToLower() == username.ToLower());

      if (userItem == null)
      {
        throw new HttpRequestException("authentication.pleaseCheckUsernameAndPassword", null, HttpStatusCode.Unauthorized);
      }

      var passwordVerified = HashingHelper.VerifyPasswordHash(loginRequest.Password, userItem.PasswordHash, userItem.PasswordSalt);

      if (!passwordVerified)
      {
        throw new HttpRequestException("authentication.pleaseCheckUsernameAndPassword", null, HttpStatusCode.Unauthorized);
      }

      var userConfig = _jwtConfig.SingleOrDefault(x => x.TokenName == "UserBearer");
      if(userConfig == null) {
        throw new HttpRequestException("serviceError.userJwtConfigurationNotFound", null, HttpStatusCode.NotFound);
      }
      var expiration = DateTime.UtcNow.AddMinutes(20);
      var jwtCreator = new JWTCreator(userConfig, userItem.UserId, new string[1] {"User"},expiration , new Dictionary<string, string>{ { "iss","LEFiles"} });
      var token = jwtCreator.GenerateToken();
      var response = new UserLoginResponse
      {
        Expiration = expiration,
        Token = token
      };
      return new SuccessDataResult<UserLoginResponse>(response);
    }

    public IResult Register(UserRegisterRequest registerRequest)
    {
      var userItem = _context.Users.SingleOrDefault(x => x.Username.ToLower() == registerRequest.Username.ToLower() || x.Email.ToLower() == registerRequest.Email.ToLower());
      if (userItem != null)
      {
        throw new HttpRequestException("", null, HttpStatusCode.Conflict);
      }
      byte[] passwordHash;
      byte[] passwordSalt;
      HashingHelper.CreatePasswordHash(registerRequest.Password, out passwordHash, out passwordSalt);
      var user = new User
      {
        Email = registerRequest.Email.ToLowerInvariant(),
        Firstname = registerRequest.Firstname,
        Lastname = registerRequest.Lastname,
        RegistrationDate = DateTime.UtcNow,
        UserId = Guid.NewGuid().ToString(),
        Username = registerRequest.Username.ToLowerInvariant(),
        PasswordHash = passwordHash,
        PasswordSalt = passwordSalt
      };
      _context.Users.Add(user);
      _context.SaveChanges();
      return new SuccessResult();
    }
  }
}
