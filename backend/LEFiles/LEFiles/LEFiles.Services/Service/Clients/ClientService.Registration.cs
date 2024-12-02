using LEFiles.Models.Entities;
using LEFiles.Services.ServiceModels.Clients.Requests;
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
    public bool RegisterClient(RegisterClientRequest registerClientRequest)
    {
      var registrationToken = _context.ClientRegistrationTokens.SingleOrDefault(x => x.Token == registerClientRequest.Token && x.Secret == x.Secret);
      if (registrationToken == null)
      {
        throw new HttpRequestException("", null, HttpStatusCode.NotFound);
      }
      var client = new Client(registrationToken.ClientName, registrationToken.UserId, registrationToken.Secret, registerClientRequest.OperatingSystem, registerClientRequest.HarddiskSN, true, DateTime.UtcNow);
      _context.Add(client);
      _context.SaveChanges();
      return true;
    }
  }
}
