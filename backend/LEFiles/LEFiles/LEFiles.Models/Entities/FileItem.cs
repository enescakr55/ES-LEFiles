using LEFiles.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Entities
{
  public class FileItem
  {
    public string FileId { get; set; } = string.Empty;
    public string? FileUploadId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public FileAttributeFlags FileAttribute { get; set; }
    public bool Shared { get; set; }
    public string? ParentFolderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual User? User { get; set; }
    public virtual FileUploadItem? FileUploadItem { get; set; }
    public virtual FolderItem? ParentFolder { get; set; }
  }
}
