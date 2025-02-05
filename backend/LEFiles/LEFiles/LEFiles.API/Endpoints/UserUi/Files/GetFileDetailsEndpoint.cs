using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Services.ServiceModels.UserInterface.Files.Responses;
using LEFiles.Services.Tools;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Files
{
  public class GetFileDetailsEndpoint : BaseEndpointWithoutRequest<IDataResult<FileDetailsResponse>>
  {
    private readonly AppDbContext _context;

    public GetFileDetailsEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Get(ApiUrl + "my-cloud/files/{fileId}/details");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      var fileId = Route<string>("fileId",true);
      var userId = User.GetUserId();
      if(userId == null){
        await SendErrorResult(401);
        return;
      }
      var file = _context.FileItems.SingleOrDefault(x => x.FileId == fileId && x.UserId == userId);
      if(file == null){
        await SendErrorResult(404);
        return;
      }
      var currentlySharing = await _context.SharedItems.Where(x => x.ItemId == fileId && x.Type == Models.Enums.SharedItemTypesEnum.FILE && (x.EndDate == null || x.EndDate > DateTime.UtcNow)).CountAsync();
      var result = new FileDetailsResponse
      {
        CreatedAt = file.CreatedAt,
        Extension = file.Extension,
        FileId = file.FileId,
        FileName = file.FileName,
        Icon = FileTools.GetIcon(file.Extension),
        FileSize = file.FileSize,
        Preview = FileTools.IsAllowPreview(file.Extension),
        Shared = (file.Shared == true && currentlySharing > 0) ? true : false,
        Thumbnail = FileTools.IsThumbnailExists(file.FileId)
      };
      await SendAsync(new SuccessDataResult<FileDetailsResponse>(result));

    }
  }
}
