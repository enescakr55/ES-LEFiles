using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.Clients.Responses
{
  public class GetMyClientRegistrationTokensResponse
  {
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public string DeviceName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime Expiration { get; set; }
  }
}
