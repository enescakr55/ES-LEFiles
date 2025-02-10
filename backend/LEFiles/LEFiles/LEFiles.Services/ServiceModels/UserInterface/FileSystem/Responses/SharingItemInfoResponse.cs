using LEFiles.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.FileSystem.Responses
{
  public class SharingItemInfoResponse
  {
    public string Name { get; set; } = string.Empty;
    public long? Size { get; set; } = 0;
    public SharedItemTypesEnum Type { get; set; }
    public string Icon { get; set; } = string.Empty;
    public string? Extension { get; set; }


  }
}
