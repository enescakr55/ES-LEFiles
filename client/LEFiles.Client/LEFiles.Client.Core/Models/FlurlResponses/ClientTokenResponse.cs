using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.Models.FlurlResponses
{
  public class ClientTokenResponse
  {
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
  }
}
