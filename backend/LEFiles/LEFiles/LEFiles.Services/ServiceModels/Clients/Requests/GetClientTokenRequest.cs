using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.Clients.Requests
{
  public class GetClientTokenRequest
  {
    public string Secret { get; set; } = string.Empty;
    public string HarddiskSN { get; set; } = string.Empty;
  }
}
