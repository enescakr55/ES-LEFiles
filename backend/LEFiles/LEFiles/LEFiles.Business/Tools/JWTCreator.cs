using LEFiles.Models.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Business.Tools
{
  public class JWTCreator
  {
    public string UserId { get; }
    public string Role { get; }
    public DateTime Expiration { get; }
    public Dictionary<string, string> AdditionalClaims { get; }
    private JWTConfig JWTConfig { get; }
    public JWTCreator(JWTConfig jwtConfig, string userId, string role, DateTime expiration, Dictionary<string, string> additionalClaims)
    {
      UserId = userId;
      Role = role;
      Expiration = expiration;
      AdditionalClaims = additionalClaims;
      JWTConfig = jwtConfig;
    }
    public string GenerateToken()
    {
      SecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTConfig.UserRSAPublicKey));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
      var claims = new List<Claim>();
      var tokenHandler = new JwtSecurityTokenHandler();
      claims.Add(new Claim(ClaimTypes.NameIdentifier, UserId));
      if (Role != null)
      {
        claims.Add(new Claim(ClaimTypes.Role, Role));
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
