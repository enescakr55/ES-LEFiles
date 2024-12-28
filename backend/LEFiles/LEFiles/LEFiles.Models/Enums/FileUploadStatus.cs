using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Enums
{
  public enum FileUploadStatus
  {
  ENTRYCREATED = 0,
  UPLOADING = 1,
  PROCESSING = 2,
  UPLOADED = 3,
  CANCELLED = 4,
  FAILED = 5,
  READY = 6

  }
}
