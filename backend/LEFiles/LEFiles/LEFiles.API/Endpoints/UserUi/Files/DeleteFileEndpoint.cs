using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Services.ServiceModels.UserInterface.Files.Requests;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Files
{
  public class DeleteFileEndpoint : BaseEndpoint<DeleteFileRequest, IResult>
  {
    private readonly AppDbContext _context;
    public DeleteFileEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Delete(ApiUrl + "my-cloud/files/{fileId}/delete");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(DeleteFileRequest req, CancellationToken ct)
    {
      var fileId = Route<string>("fileId");
      var userId = User.GetUserId();

      if (userId == null)
      {
        await SendErrorResult(401);
        return;
      }
      if(req.Confirm != true){
        await SendErrorResult(400);
        return;
      }
      if (req.File != fileId)
      {
        await SendErrorResult(400);
        return;
      }

      var fileItem = await _context.FileItems.Include(x => x.FileUploadItem).SingleOrDefaultAsync(x => x.FileId == req.File && x.UserId == userId);
      if (fileItem == null || fileItem.FileUploadItem == null)
      {
        await SendErrorResult(404);
        return;
      }
      if (fileItem.FileUploadItem.FilePath != null)
      {
        var fileExists = File.Exists(fileItem.FileUploadItem.FilePath);
        if (fileExists)
        {
          File.Delete(fileItem.FileUploadItem.FilePath);
        }
      }
      if(!string.IsNullOrEmpty(fileItem.ParentFolderId)){
        var folderItem = await _context.FolderItems.SingleOrDefaultAsync(x => x.FolderId == fileItem.ParentFolderId && x.UserId == userId);
        if(folderItem != null){
          folderItem.LastUpdatedAt = DateTime.UtcNow;
          _context.Update(folderItem);
        }
      }
      
     
      var fileUploadItem = fileItem.FileUploadItem;
      _context.Remove(fileItem);
      _context.Remove(fileUploadItem);
      await _context.SaveChangesAsync();
      await SendAsync(new SuccessResult());
    }
  }
}
