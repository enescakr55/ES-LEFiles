using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Services.ServiceModels.UserInterface.Folders.Requests;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Folders
{
  public class DeleteFolderEndpoint : BaseEndpoint<DeleteFolderRequest,IResult>
  {
    private readonly AppDbContext _context;

    public DeleteFolderEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Delete(ApiUrl + "my-cloud/folders/{folderId}/delete");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(DeleteFolderRequest req, CancellationToken ct)
    {
      var userId = User.GetUserId();
      if(userId == null){
        await SendErrorResult(401);
        return;
      }
      var folderId = Route<string>("folderId");
      if(folderId == null || req.FolderId != folderId){
        await SendErrorResult(400);
        return;
      }
      if(req.Confirm == false){
        await SendErrorResult(400, "common.checkConfirm");
        return;
      }
      var folder = await _context.FolderItems.SingleOrDefaultAsync(x => x.FolderId == folderId);
      if(folder == null){
        await SendErrorResult(404,"Folder not found");
        return;
      }
      _context.FolderItems.Remove(folder);
      await _context.SaveChangesAsync();
      await SendAsync(new SuccessResult());
    }
  }
}
