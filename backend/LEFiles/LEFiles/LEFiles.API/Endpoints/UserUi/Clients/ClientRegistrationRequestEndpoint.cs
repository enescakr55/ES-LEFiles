using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Helpers;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Models.Entities;
using LEFiles.Services.ServiceModels.Authentication.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LEFiles.API.Endpoints.UserUi.Clients
{
  public class ClientRegistrationRequestEndpoint : BaseEndpoint<ClientRegistrationRequest, IResult>
  {
    private readonly AppDbContext _context;

    public ClientRegistrationRequestEndpoint(AppDbContext context)
    {
      _context = context;
    }
    public override void Configure()
    {
      Post(ApiUrl + "clients/client-registration/new");
      AuthSchemes("UserBearer", "ClientBearer");
      Roles("User");
    }
    public override async Task HandleAsync(ClientRegistrationRequest req, CancellationToken ct)
    {
      var userId = User.GetUserId();
      if (userId == null)
      {
        await SendErrorResult(400);
        return;
      }
      var roles = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role);
      var secret = RandomStringGenerator.Create(15);
      var token = RandomStringGenerator.Create(50);
      var clientRegistrationToken = new ClientRegistrationToken
      {
        CreatedAt = DateTime.UtcNow,
        ExpirationDate = DateTime.UtcNow.AddMinutes(15),
        Secret = secret,
        Token = token,
        UserId = userId,

      };
      await _context.AddAsync(clientRegistrationToken, ct);
      await _context.SaveChangesAsync(ct);
      await SendAsync(new SuccessResult());
    }
  }
}
