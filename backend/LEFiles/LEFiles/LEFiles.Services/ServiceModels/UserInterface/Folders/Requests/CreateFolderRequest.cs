using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.Folders.Requests
{
  public class CreateFolderRequest
  {
        public string FolderName { get; set; } = string.Empty;
        public string? ParentFolder { get; set; }
        public bool Shared { get; set; } = false;
    }
}
