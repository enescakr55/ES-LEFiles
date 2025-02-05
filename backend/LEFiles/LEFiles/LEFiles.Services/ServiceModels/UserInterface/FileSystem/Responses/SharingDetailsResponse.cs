using LEFiles.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.FileSystem.Responses
{
  public class SharingDetailsResponse
  {
    public string Name { get; set; } = string.Empty;
    public SharedItemAccessTypesEnum Access { get; set; }
    public DateTime? End { get; set; }
    public List<SharingDetailsUsersResponse>? Users { get; set; }
    public class SharingDetailsUsersResponse
    {
      public string Id { get; set; } = string.Empty;
      public string UserName { get; set; } = string.Empty;
    }
  }

}
