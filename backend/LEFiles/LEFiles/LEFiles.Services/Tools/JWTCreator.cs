using LEFiles.Models.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.Tools
{
  public class JWTCreator
  {
    public string UserId { get; }
    public string[] Roles { get; }
    public DateTime Expiration { get; }
    public Dictionary<string, string> AdditionalClaims { get; }
    private JWTConfig JWTConfig { get; }
    public JWTCreator(JWTConfig jwtConfig, string userId, string[] roles, DateTime expiration, Dictionary<string, string> additionalClaims)
    {
      UserId = userId;
      Roles = roles;
      Expiration = expiration;
      AdditionalClaims = additionalClaims;
      JWTConfig = jwtConfig;
    }
    public string GenerateToken()
    {
      var rsa = new RSACryptoServiceProvider(2048);
      rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(JWTConfig.PrivateKey),out _);
      var secureKey = new RsaSecurityKey(rsa);
      var credentials = new SigningCredentials(secureKey, SecurityAlgorithms.RsaSha256);
      var claims = new List<Claim>();
      var tokenHandler = new JwtSecurityTokenHandler();
      claims.Add(new Claim("sub", UserId));
      if (Roles != null && Roles.Length > 0)
      {
        foreach (var role in Roles)
        {
          claims.Add(new Claim(ClaimTypes.Role, role));
        }

      }
      if (AdditionalClaims != null)
      {
        foreach (var additionalClaim in AdditionalClaims)
        {
          claims.Add(new Claim(additionalClaim.Key, additionalClaim.Value));
        }
      }

      var tokenDescriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(claims),
        Expires = Expiration,
       // Issuer = Issuer,
        SigningCredentials = credentials
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}
