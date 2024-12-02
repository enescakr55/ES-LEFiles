using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.Models.FlurlRequests
{
  public class ClientTokenRequest
  {
    public string Secret { get; set; } = string.Empty;
    public string HarddiskSN { get; set; } = string.Empty;
  }
}
