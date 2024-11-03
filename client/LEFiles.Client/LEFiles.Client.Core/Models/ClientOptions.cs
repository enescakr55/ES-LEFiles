using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.Models
{
  public class ClientOptions
  {
    public bool AllowFileUpload { get; set; } = false;
    public bool AllowShowHiddenFiles { get; set; } = false;
    public bool AllowShowSystemFiles { get; set; } = false;
    }
}
