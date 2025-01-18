using Global.CoreProject.Extensions;
using LEFiles.Common.Constraints;
using LEFiles.Core.Endpoints;
using LEFiles.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Files
{
  public class ViewFilePreviewEndpoint : BaseEndpointWithoutRequest<object>
  {
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public ViewFilePreviewEndpoint(AppDbContext context, IConfiguration configuration)
    {
      _context = context;
      _configuration = configuration;
    }

    public override void Configure()
    {
      Get(ApiUrl + "my-cloud/files/{fileId}/preview");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      var fileType = "";
      var userId = User.GetUserId();
      if(userId == null) {
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
      var filePath = fileUpload.FilePath;
      if(fileUpload.Extension != null){
        var isImage = FileExtensionTypes.Images.Any(x => x.ToLowerInvariant() == fileUpload.Extension.ToLowerInvariant());
        var isAudio = FileExtensionTypes.Audios.Any(x => x.ToLowerInvariant() == fileUpload.Extension.ToLowerInvariant());
        if(isImage == true){
          fileType = "image";
        }else if(isAudio == true){
          fileType = "audio";
        }
      }

      if (fileUpload != null && fileUpload.FilePath != null && fileType == "image" && File.Exists(fileUpload.FilePath))
      {
        var fileInfo = new FileInfo($"{filePath}");
        await SendFileAsync(fileInfo, "image/png", null, true, ct);
        return;
      }else if(fileUpload != null && fileUpload.FilePath != null && fileType == "audio" && File.Exists(fileUpload.FilePath)){
        var fileInfo = new FileInfo($"{filePath}");
        await SendFileAsync(fileInfo, "audio/mpeg", null, true, ct);
        return;
      }
      await SendErrorResult(500);
      return;
    }

  }
}
