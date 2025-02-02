using LEFiles.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.Files.Requests
{
  public class ShareFileRequest
  {
    public string FileId { get; set; } = string.Empty;
    public DateTime? End { get; set; } = null;
    public SharedItemAccessTypesEnum Access { get; set; }
    public string? Users = null;
  }
}
