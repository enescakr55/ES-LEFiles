using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Services.ServiceModels.UserInterface.Folders.Requests;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Folders
{
  public class UpdateFolderEndpoint : BaseEndpoint<UpdateFolderRequest, IResult>
  {
    private readonly AppDbContext _context;

    public UpdateFolderEndpoint(AppDbContext context)
    {
      _context = context;
    }
    public override void Configure()
    {
      Post(ApiUrl + "my-cloud/folders/{folderId}/update");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(UpdateFolderRequest req, CancellationToken ct)
    {
      var userId = User.GetUserId();
      if (userId == null) {
        await SendErrorResult(401);
        return;
      }
      var folderId = Route<string>("folderId");
      if(folderId == null || folderId != req.FolderId) {
        await SendErrorResult(400);
        return;
      }
      var folder = await _context.FolderItems.SingleOrDefaultAsync(x => x.FolderId == folderId);
      if (folder == null) {
        await SendErrorResult(404);
        return;
      }
      var folderControl = await _context.FolderItems.SingleOrDefaultAsync(x => x.FolderId != folderId && (x.FolderName.ToLower() == req.FolderName.ToLower() && x.ParentFolderId == folder.ParentFolderId));
      if(folderControl != null) {
        await SendErrorResult(409);
        return;
      }
      folder.Shared = req.Shared;
      folder.FolderName = req.FolderName;
      folder.LastUpdatedAt = DateTime.UtcNow;
      _context.Update(folder);
      await _context.SaveChangesAsync();
      await SendAsync(new SuccessResult());
    }
  }
}
