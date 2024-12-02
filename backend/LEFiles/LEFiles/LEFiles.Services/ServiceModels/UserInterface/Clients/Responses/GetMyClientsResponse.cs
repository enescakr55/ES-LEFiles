using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.Clients.Responses
{
  public class GetMyClientsResponse
  {
    public string Id { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string OperatingSystem { get; set; } = string.Empty;
    public string IsActive { get; set; } = string.Empty;
  }
}
