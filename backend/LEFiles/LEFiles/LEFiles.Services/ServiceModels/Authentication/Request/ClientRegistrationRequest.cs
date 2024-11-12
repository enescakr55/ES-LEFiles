using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.Authentication.Request
{
  public class ClientRegistrationRequest
  {
    public string ClientName { get; set; } = string.Empty;
    //public int ValidityPeriod { get; set; }
  }
}
