using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Common.Constraints
{
  public class FileExtensionIconModel
  {
    public string[] Extensions { get; set; } = new string[0];
    public string Icon { get; set; } = string.Empty;
  }
  public static class FileExtensionIcons
  {
    public static List<FileExtensionIconModel> Icons = new List<FileExtensionIconModel>
    {
      new FileExtensionIconModel {
        Extensions = FileExtensionTypes.Images.ToArray(),
        Icon = "bi bi-image-fill"
      },
      new FileExtensionIconModel {
        Extensions = FileExtensionTypes.Audios.ToArray(),
        Icon = "bi bi-file-earmark-music-fill"
      },
      new FileExtensionIconModel {
        Extensions = FileExtensionTypes.Videos.ToArray(),
        Icon = "bi bi-film"
      },
      new FileExtensionIconModel {
        Extensions = new string[] {".zip",".rar",".7z",".gzip",".tar",".bzip2" },
        Icon = "bi bi-file-zip-fill"
      },      
      new FileExtensionIconModel {
        Extensions = new string[] {".iso" },
        Icon = "bi bi-disc-fill"
      },new FileExtensionIconModel {
        Extensions = new string[] {".exe" },
        Icon = "bi bi-filetype-exe"
      }
    };
  }
}
