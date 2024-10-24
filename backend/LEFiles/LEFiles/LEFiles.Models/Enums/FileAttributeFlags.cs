using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Enums
{
  [Flags]
  public enum FileAttributeFlags
  {
  None = 0,
  Hidden = 1,
  SystemFile = 2,
  Readonly = 4,

  }
}
