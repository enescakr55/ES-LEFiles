using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Configuration
{
  public class JWTConfig
  {
    public string Issuer { get; set; }
    public string ClientRSAPublicKey { get; set; }
    public string ClientRSAPrivateKey { get; set; }
    public string UserRSAPublicKey { get; set; }
    public string UserRSAPrivateKey { get; set; }
  }
}
