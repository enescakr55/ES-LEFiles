using LEFiles.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.FileUploadProcessors.Abstract
{
  public abstract class FileUploadProcessorBase
  {
    private bool Configured;
    protected FileItem? FileItem;
    protected List<string>? AllowedFileExtensions;
    public void Start(){
      if(Configured == false) {
        throw new Exception("Not configured");
      }
      if(FileItem == null) {
        throw new Exception("File Item not found");
      }
      if(AllowedFileExtensions != null) {
        var uploadedFileExtension = FileItem.Extension.ToLower();
        var isAllowed = AllowedFileExtensions.Any(x => x.ToLower() == uploadedFileExtension);
        if(!isAllowed){
          return;
        }
      }
      Process();
    }
    public void SetFileItem(FileItem fileItem){
      FileItem = fileItem;
    }
    public void Configure(List<string> allowedFileExtensions)
    {
      AllowedFileExtensions = allowedFileExtensions;
      Configured = true;
    }
    public abstract void Process();
  }
}
