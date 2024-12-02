using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Models.Configuration;
using LEFiles.Services.ServiceModels.Clients.Requests;
using LEFiles.Services.ServiceModels.Clients.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.Service.Clients
{
  public partial class ClientService
  {


    public IDataResult<ClientTokenResponse> GetClientToken(GetClientTokenRequest tokenRequest)
    {
      var client = _context.Clients.SingleOrDefault(x => x.ClientSecret == tokenRequest.Secret && x.HarddiskSerialNumber == tokenRequest.HarddiskSN.Trim());
      if(client == null) {
        throw new HttpRequestException("", null,HttpStatusCode.NotFound);
      }
      var response = _authenticationService.ClientLogin(tokenRequest);
      return response;
    }
  }
}
