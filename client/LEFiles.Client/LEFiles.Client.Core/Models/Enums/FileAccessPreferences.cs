using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.Models.Enums
{
  [Flags]
  public enum FileAccessPreferences
  {
    None = 0,
    Read = 1,
    Write = 2,
    Update = 4,
    Delete = 8
  }
}
