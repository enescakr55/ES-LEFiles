using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Common.Constraints
{
  public static class FileExtensionTypes
  {
    public static string[] Images = { ".jpg", ".jpeg", ".png", ".bmp", ".webp", ".gif", ".tiff", ".tif" };
    public static string[] Videos = { ".mp4",".mov",".wmv",".avi",".avchd",".mkv" };
    public static string[] Audios = {".m4a",".mp3",".wav" };
    public static string[] Documents = { ".pdf",".xls",".xlsx",".docx",".doc",".ppt",".pptx",".txt" };
    /* Yalnızca butonların görüntülenmesi için */
    public static string[] Viewables = { ".jpg", ".jpeg", ".png", ".bmp", ".webp", ".mp4", ".avi", ".pdf", ".txt", ".m4a", ".mp3", ".wav" };
    public static string[] ShowDetailPreview = { ".jpg", ".jpeg", ".png", ".bmp", ".webp" };
    public static string[] Editables = { ".txt" };
    //

  }
}
