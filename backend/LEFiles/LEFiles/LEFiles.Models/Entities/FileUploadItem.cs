using LEFiles.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Entities
{
  public class FileUploadItem
  {
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? FileName { get; set; }
    public string? Extension { get; set; }
    public long? FileSize { get; set; }
    public string? FilePath { get; set; } = string.Empty;
    public FileUploadStatus Status { get; set; } = FileUploadStatus.ENTRYCREATED;
    public int Provider { get; set; } = 0; // 0 : Local
    public DateTime CreatedAt { get; set; }
    public DateTime UploadedAt { get; set; }
  }
}
