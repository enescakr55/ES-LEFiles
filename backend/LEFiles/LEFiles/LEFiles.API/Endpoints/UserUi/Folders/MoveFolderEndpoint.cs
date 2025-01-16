using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Models.Entities;
using LEFiles.Services.ServiceModels.UserInterface.Folders.Requests;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Folders
{
  public class MoveFolderEndpoint : BaseEndpoint<MoveFolderRequest,IResult>
  {
    private readonly AppDbContext _context;

    public MoveFolderEndpoint(AppDbContext context)
    {
      _context = context;
    }
    public override void Configure()
    {
      Post(ApiUrl+"my-cloud/folders/{source}/move");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(MoveFolderRequest req, CancellationToken ct)
    {
      var sourceId = Route<string>("source");
      var targetId = req.TargetFolderId;
      if(sourceId == targetId){
        await SendErrorResult(400);
        return;
      }
      if (sourceId != req.SourceFolderId) {
        await SendErrorResult(400);
        return;
      }
      var userId = User.GetUserId();
      if(userId == null){
        await SendErrorResult(401);
        return;
      }
      var sourceFolder = await _context.FolderItems.SingleOrDefaultAsync(x => x.FolderId == sourceId && x.UserId == userId);
      if (sourceFolder == null){
        await SendErrorResult(404);
        return;
      }
      if (targetId == null)
      {
        sourceFolder.ParentFolderId = null;
      }else{
        var targetFolder = await _context.FolderItems.SingleOrDefaultAsync(x => x.FolderId == targetId && x.UserId == userId);
        if(targetFolder == null){
          await SendErrorResult(404,"Target Folder not found");
          return;
        }
        if (targetFolder.ParentFolderId == null)
        {
          sourceFolder.ParentFolderId = targetFolder.FolderId;
        }else{
          FolderItem? tempFolder = targetFolder;
          do
          {
            tempFolder = await _context.FolderItems.SingleOrDefaultAsync(x => x.FolderId == targetFolder.ParentFolderId && x.UserId == userId);
            if (tempFolder == null)
            {
              await SendErrorResult(404, "Parent folder not found");
              return;
            }
            if(tempFolder.FolderId == sourceId || tempFolder.ParentFolderId == sourceId){
              await SendErrorResult(400);
              return;
            }
          } while (tempFolder.ParentFolderId != null);
          sourceFolder.ParentFolderId = targetId;
        }
      }
      _context.Update(sourceFolder);
      await _context.SaveChangesAsync();
      await SendAsync(new SuccessResult("Folder successfully moved"));
    }
  }
}
