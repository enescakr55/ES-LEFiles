using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Configuration
{
  public class JWTConfig
  {
    public string Issuer { get; set; } = string.Empty;
    public string TokenName { get; set; } = string.Empty;
    public string PublicKey { get; set; } = string.Empty;
    public string PrivateKey { get; set; } = string.Empty;
  }
}
