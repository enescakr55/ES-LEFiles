using LEFiles.Client.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.Models
{
  public class FileSystemEntries
  {
    public string? FullPath { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public EntryTypes EntryType { get; set; }
    public DrivePropertiesItem? DriveProperties { get; set; }
    public FilePropertiesItem? FileProperties { get; set; }
    public FolderPropertiesItem? FolderProperties { get; set; }
    public class DrivePropertiesItem
    {
      public string Letter { get; set; } = string.Empty;
      public string Format { get; set; } = string.Empty;
      public string DriveType { get; set; } = string.Empty;
      public long TotalSize { get; set; }
      public long FreeSpace { get; set; }
    }
    public class FolderPropertiesItem
    {
      public DateTime LastAccess { get; set; }
      public DateTime LastWrite { get; set; }
    }
    public class FilePropertiesItem
    {
      public string Extension { get; set; } = string.Empty;
      public long Size { get; set; }
    }
  }
}
