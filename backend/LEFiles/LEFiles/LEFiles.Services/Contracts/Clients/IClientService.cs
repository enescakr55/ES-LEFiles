using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Services.ServiceModels.Clients.Requests;
using LEFiles.Services.ServiceModels.Clients.Responses;
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
    public IDataResult<ClientTokenResponse> GetClientToken(GetClientTokenRequest tokenRequest);
  }
}
