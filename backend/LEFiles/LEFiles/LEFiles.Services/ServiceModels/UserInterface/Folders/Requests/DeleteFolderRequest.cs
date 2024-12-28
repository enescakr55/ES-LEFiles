using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.Folders.Requests
{
  public class DeleteFolderRequest
  {
        public string FolderId { get; set; } = string.Empty;
        public bool Confirm { get; set; }
    }
}
