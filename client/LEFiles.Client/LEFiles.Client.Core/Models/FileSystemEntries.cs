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
    public string FullPath { get; set; }
    public string Name { get; set; }
    public EntryTypes EntryType { get; set; }
  }
}
