using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Services.ServiceModels.UserInterface.Folders.Responses;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Folders
{
  public class UpdateFolderContentsEndpoint : BaseEndpointWithoutRequest<IDataResult<UpdateFolderContentResponse>>
  {
    private AppDbContext _context;

    public UpdateFolderContentsEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Get(ApiUrl + "my-cloud/folders/{folderId}/update");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      var userId = User.GetUserId();
      if(userId == null) {
        await SendErrorResult(401);
        return;
      }
      var folderId = Route<string>("folderId");
      if(string.IsNullOrEmpty(folderId)){
        await SendErrorResult(400,"Bad Request");
        return;
      }
      var folder = await _context.FolderItems.SingleOrDefaultAsync(x => x.FolderId == folderId && x.UserId == userId);
      if(folder == null){
        await SendErrorResult(404, "Folder not found");
        return;
      }
      var result = new UpdateFolderContentResponse
      {
        FolderId = folder.FolderId,
        FolderName = folder.FolderName,
        Shared = folder.Shared
      };
      await SendAsync(new SuccessDataResult<UpdateFolderContentResponse>(result));
    }
  }
}
