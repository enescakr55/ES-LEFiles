using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.Folders.Requests
{
  public class MoveFolderRequest
  {
    //SourceFolder TargetFolder'a taşınacak.  TargetFolderId boş olursa ana dizine taşınır
    public string SourceFolderId { get; set; } = string.Empty;
    public string? TargetFolderId { get; set; } = string.Empty;
  }
}
