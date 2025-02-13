using LEFiles.Models.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;

namespace LEFiles.API.JwtTokenValidator
{
  public class JwtValidationService
  {
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IConfiguration _configuration;
    private readonly List<JWTConfig> _jwtConfig;
    public JwtValidationService(IHttpContextAccessor contextAccessor, IConfiguration configuration, List<JWTConfig> jwtConfig)
    {
      _contextAccessor = contextAccessor;
      _configuration = configuration;
      _jwtConfig = jwtConfig;
    }
    public string? GetUserId(){
      try {
        var context = _contextAccessor.HttpContext;
        if (context == null)
        {
          return null;
        }
        var jwtToken = context.Request.Headers["Authorization"].ToString().Split("Bearer ")[1];
        if (jwtToken == null || jwtToken == "")
        {
          return null;
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();
        SecurityToken validatedToken;
        IPrincipal principal = tokenHandler.ValidateToken(jwtToken, validationParameters,out validatedToken);
        if (principal.Identity != null && principal.Identity.IsAuthenticated) {
          var readedToken = tokenHandler.ReadJwtToken(jwtToken);
          return readedToken.Subject;
        }
        return null;
      }
      catch{
      return null;
      }
    }
    private TokenValidationParameters GetValidationParameters(){
      var configuration = _jwtConfig.SingleOrDefault(x=>x.TokenName == "UserBearer");
      if(configuration == null){
        throw new HttpRequestException("", null, HttpStatusCode.InternalServerError);
      }
      RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
      rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(configuration.PublicKey), out _);
      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
      return new TokenValidationParameters
      {
        ValidateIssuer = false,
        ValidateAudience = false,
        RoleClaimType = "role",
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration.Issuer,
        IssuerSigningKey = new RsaSecurityKey(rsa),
        ClockSkew = TimeSpan.FromSeconds(60)
      };
    }

  }
}
