using LEFiles.Common.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.Tools
{
  public static class FileTools
  {
    public static string GetIcon(string extension)
    {
      var iconModel = FileExtensionIcons.Icons.Where(x => x.Extensions.Contains(extension)).FirstOrDefault();
      if (iconModel == null)
      {
        return "bi bi-file-earmark-fill";
      }
      else
      {
        return iconModel.Icon;
      }
    }
    public static bool IsAllowPreview(string extension)
    {
      if (extension != null)
      {
        var isImage = FileExtensionTypes.Images.Any(x => x.ToLowerInvariant() == extension.ToLowerInvariant());
        if (isImage == true)
        {
          return true;
        }
      }
      return false;
    }
    public static bool IsThumbnailExists(string fileItemId)
    {
      var thumbnailPath = $"/esaycloud/data/thumbnails/{fileItemId}.png";
      return File.Exists(thumbnailPath);
    }
  }
}
