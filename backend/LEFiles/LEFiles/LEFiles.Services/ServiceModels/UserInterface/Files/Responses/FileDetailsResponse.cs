using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.Files.Responses
{
  public class FileDetailsResponse
  {
    public string FileId { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public bool Preview { get; set; } = false;
    public bool Thumbnail { get; set; } = false;
    public long FileSize { get; set; }
    public bool Shared { get; set; } = false;
    public DateTime CreatedAt { get; set; }
  }
}
