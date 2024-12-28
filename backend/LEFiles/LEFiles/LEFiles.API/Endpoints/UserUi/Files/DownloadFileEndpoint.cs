using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Files
{
  public class DownloadFileEndpoint : BaseEndpointWithoutRequest<object>
  {
    private readonly AppDbContext _context;

    public DownloadFileEndpoint(AppDbContext context)
    {
      _context = context;
    }
    public override void Configure()
    {
      Get(ApiUrl + "my-cloud/files/{fileId}/download");
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
      var fileId = Route<string?>("fileId");
      if(fileId == null){
        await SendErrorResult(400);
        return;
      }
      var file = await _context.FileItems.Include(x=>x.FileUploadItem).SingleOrDefaultAsync(x => x.FileId == fileId && x.UserId == userId);
      if(file == null){
        await SendErrorResult(404);
        return;
      }
      var fileUploadItem = file.FileUploadItem;
      if(fileUploadItem == null || fileUploadItem.Status != Models.Enums.FileUploadStatus.UPLOADED){
        await SendErrorResult(404);
        return;
      }
      if(fileUploadItem.FilePath != null){
        var fileInfo = new FileInfo(fileUploadItem.FilePath);
        await SendFileAsync(fileInfo, "application/octet-stream", null, true, ct);
        return;
      }
      await SendErrorResult(404);



    }
  }
}
