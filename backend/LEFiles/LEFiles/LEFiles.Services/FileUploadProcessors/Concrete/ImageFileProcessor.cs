using LEFiles.Common.Constraints;
using LEFiles.Services.FileUploadProcessors.Abstract;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.FileUploadProcessors.Concrete
{
  public class ImageFileProcessor : FileUploadProcessorBase
  {
    public ImageFileProcessor()
    {
    Configure(FileExtensionTypes.Images.ToList());
    }
    public override void Process()
    {
      
      var fileItem = FileItem;
      if (fileItem == null)
      {
        throw new Exception();
      }
      var scaleFactor = 0.3;
      var path = "/esaycloud/data/thumbnails/";
      var fileStream = new FileStream(fileItem.FilePath ?? "", FileMode.Open);
      using var codec = SKCodec.Create(fileStream);
      using var bitmap = SKBitmap.Decode(codec);
      //int newWidth = (int)(bitmap.Width * scaleFactor);
      //int newHeight = (int)(bitmap.Height * scaleFactor);
      var currentHeight = bitmap.Height;
      var currentWidth = bitmap.Width;
      decimal scale = bitmap.Width > 100 ? Math.Round((decimal)((decimal)130 / bitmap.Width),2) : 1;
      if(scale == 0){
        scale = 0.1M;
      }
      int newWidth = (int)(bitmap.Width * scale);
      int newHeight = (int)(bitmap.Height * scale);

      using var scaledBitmap = bitmap.Resize(new SKSizeI(newWidth, newHeight), SKSamplingOptions.Default);
      //var scaledImage = scaledBitmap.Encode(codec.EncodedFormat, 50);
      var scaledImage = scaledBitmap.Encode(SKEncodedImageFormat.Png, 50);
      Directory.CreateDirectory(path);
      Stream fs = new FileStream(path + fileItem.FileId+".png", FileMode.Create);
      scaledImage.AsStream().CopyTo(fs);
      fs.Close();



    }
  }
}
