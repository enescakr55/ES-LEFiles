using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.Folders.Responses
{
  public class FileSystemEntrySearchResult
  {
    public string SearchText { get; set; } = string.Empty;
    public List<FileSystemEntryResponse> Result { get; set; } = new();
  }
}
