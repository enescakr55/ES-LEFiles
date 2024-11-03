using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.Models.Enums
{
  [Flags]
  public enum FileSystemEntryOptions
  {
  None = 0,
  ShowHiddenFiles = 1,
  ShowSystemFiles = 2
  }
}
