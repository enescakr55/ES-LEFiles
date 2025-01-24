using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Services.ServiceModels.UserInterface.Folders.Responses;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Folders
{
  public class GetFolderDetailsEndpoint : BaseEndpointWithoutRequest<IDataResult<FolderDetailsResponse>>
  {
    private readonly AppDbContext _context;

    public GetFolderDetailsEndpoint(AppDbContext context)
    {
      _context = context;
    }
    public override void Configure()
    {
      Get(ApiUrl + "my-cloud/folders/{folderId}/details");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      var userId = User.GetUserId();
      if (userId == null)
      {
        await SendErrorResult(401);
        return;
      }
      var folderId = Route<string>("folderId");
      if (folderId == null) {
        await SendErrorResult(400);
        return;
      }
      var folder = await _context.FolderItems.SingleOrDefaultAsync(x => x.FolderId == folderId && x.UserId == userId);
      if (folder == null)
      {
        await SendErrorResult(404,"Folder not found");
        return;
      }
      var folderCount = await _context.FolderItems.Where(x => x.ParentFolderId == folderId && x.UserId == userId).CountAsync();
      var fileCount = await _context.FileItems.Where(x => x.ParentFolderId == folderId && x.UserId == userId).CountAsync();
      var folderDetails = new FolderDetailsResponse
      {
        CreatedAt = folder.CreatedAt,
        FolderId = folder.FolderId,
        LastUpdatedAt = folder.LastUpdatedAt,
        Name = folder.FolderName,
        ContentCount = folderCount + fileCount
      };
      await SendAsync(new SuccessDataResult<FolderDetailsResponse>(folderDetails));
    }
  }
}
