using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Files
{
  public class GetFileThumbnailEndpoint : BaseEndpointWithoutRequest<object>
  {
    private readonly AppDbContext _context;

    public GetFileThumbnailEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Get(ApiUrl + "my-cloud/files/{fileId}/thumbnail");
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
      var fileId = Route<string>("fileId");
      if (fileId == null)
      {
        await SendErrorResult(400);
        return;
      }
      var file = await _context.FileItems.Include(x => x.FileUploadItem).SingleOrDefaultAsync(x => x.FileId == fileId && x.UserId == userId);
      if (file == null)
      {
        await SendErrorResult(404);
        return;
      }
      var fileUpload = file.FileUploadItem;
      if (fileUpload == null || fileUpload.UserId != userId)
      {
        await SendErrorResult(404);
        return;
      }
      var thumbnailPath = "/esaycloud/data/thumbnails/";
      if (fileUpload != null && fileUpload.FilePath != null)
      {
        var fileInfo = new FileInfo($"{thumbnailPath}{file.FileId}.png");
        await SendFileAsync(fileInfo, "image/png", null, true, ct);
        return;
      }
      await SendErrorResult(500);
      return;


    }
  }
}
