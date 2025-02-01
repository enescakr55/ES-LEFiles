using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.Files.Requests
{
  public class RenameFileRequest
  {
    public string FileId { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
  }
}
