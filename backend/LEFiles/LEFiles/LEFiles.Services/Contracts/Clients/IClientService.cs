using LEFiles.Services.ServiceModels.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.Contracts.Clients
{
  public interface IClientService
  {
    public bool RegisterClient(RegisterClientRequest registerClientRequest);
  }
}
