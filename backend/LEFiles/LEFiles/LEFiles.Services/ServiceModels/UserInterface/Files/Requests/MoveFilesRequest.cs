using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.Files.Requests
{
  public class MoveFilesRequest
  {
    public string[] SourceFiles { get; set; } = new string[0];
    public string? Destination { get; set; }
  }
}
