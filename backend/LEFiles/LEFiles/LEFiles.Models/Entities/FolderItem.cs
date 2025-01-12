using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Entities
{
  public class FolderItem
  {
    public string FolderId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string FolderName { get; set; } = string.Empty;
    public bool Shared { get; set; }
    public string? ParentFolderId { get; set; }
    public virtual FolderItem? ParentFolder { get; set; }
    public virtual List<FolderItem>? Childs { get; set; }
    public virtual List<FileItem>? Files { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public virtual User? User { get; set; }

  }
}
