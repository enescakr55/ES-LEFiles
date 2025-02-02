using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.UserInterface.Folders.Responses
{
  public class FileAndFoldersResponse
  {
    public List<ParentFolderEntryResponse>? Parents { get; set; }
    public List<FileSystemEntryResponse> Entries { get; set; } = new();
  }
  public class ParentFolderEntryResponse
  {
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
  }
  public class FileSystemEntryResponse
  {
    public string Id { get; set; } = string.Empty;
    public int Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Shared { get; set; }
    public string? Extension { get; set; }
    public bool? ThumbnailExists { get; set; }
    public bool? AllowPreview { get; set; } = false;
    public bool? ShowDetailPreview { get; set; }
    public string? Icon { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }

  }
}
