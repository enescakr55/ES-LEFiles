using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.Files.Requests
{
  public class DeleteFileRequest
  {
        public string File { get; set; } = string.Empty;
        public bool Confirm { get; set; } = false;
    }
}
