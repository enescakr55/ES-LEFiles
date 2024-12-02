using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.Clients.Responses
{
  public class ClientTokenResponse
  {
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
  }
}
