using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.DataAccess;
using LEFiles.Services.ServiceModels.UserInterface.FileSystem.Responses;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.FileSystem
{
  public class SharingDetailsEndpoint : BaseEndpointWithoutRequest<SharingDetailsResponse>
  {
    private readonly AppDbContext _context;

    public SharingDetailsEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Get(ApiUrl + "my-cloud/files/{fileId}/sharing-details", ApiUrl + "my-cloud/folders/{folderId}/sharing-details");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      var userId = User.GetUserId();
      if (userId == null) {
        await SendErrorResult(401);
        return;
      }
      var fileId = Route<string?>("fileId", false);
      var folderId = Route<string?>("folderId", false);
      if ((fileId == null && folderId == null) || (fileId != null && folderId != null))
      {
        await SendErrorResult(400);
        return;
      }
      var sharingDetails = new SharingDetailsResponse();
      if (fileId != null)
      {
        var sharingFile = await _context.FileItems.SingleOrDefaultAsync(x => x.FileId == fileId && x.UserId == userId && x.Shared == true);
        if(sharingFile == null){
          await SendErrorResult(404);
          return;
        }
        var sharingItem = await _context.SharedItems.FirstOrDefaultAsync(x => x.ItemId == fileId && x.Type == Models.Enums.SharedItemTypesEnum.FILE && (x.EndDate == null || x.EndDate > DateTime.UtcNow));
        if (sharingItem == null) {
          await SendErrorResult(404);
          return;
        }
        sharingDetails.Name = sharingFile.FileName;
        sharingDetails.End = sharingItem.EndDate;
        sharingDetails.Access = sharingItem.AccessType;
        if(sharingItem.AccessType == Models.Enums.SharedItemAccessTypesEnum.SPECIFIC_USERS){
          try {

          }catch{
            
          }
        }
      }
      else
      {
       // klasörler için paylaşım detayları
      }

    }
  }
}
