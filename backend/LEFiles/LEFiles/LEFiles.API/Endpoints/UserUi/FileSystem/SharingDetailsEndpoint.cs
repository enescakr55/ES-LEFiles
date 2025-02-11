using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Models.Configuration;
using LEFiles.Services.ServiceModels.UserInterface.FileSystem.Responses;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static LEFiles.Services.ServiceModels.UserInterface.FileSystem.Responses.SharingDetailsResponse;

namespace LEFiles.API.Endpoints.UserUi.FileSystem
{
  public class SharingDetailsEndpoint : BaseEndpointWithoutRequest<IDataResult<SharingDetailsResponse>>
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
        sharingDetails.AccessKey = sharingItem.AccessKey;
        sharingDetails.Access = sharingItem.AccessType;
        if(sharingItem.AccessType == Models.Enums.SharedItemAccessTypesEnum.SPECIFIC_USERS){
          sharingDetails.Users = GetUsers(sharingItem.Data);
        }
        await SendAsync(new SuccessDataResult<SharingDetailsResponse>(sharingDetails));
        return;
      }
      else
      {
        var sharingFolder = await _context.FolderItems.SingleOrDefaultAsync(x => x.FolderId == folderId && x.UserId == userId && x.Shared == true);
        if (sharingFolder == null)
        {
          await SendErrorResult(404);
          return;
        }
        var sharingItem = await _context.SharedItems.FirstOrDefaultAsync(x => x.ItemId == folderId && x.Type == Models.Enums.SharedItemTypesEnum.FOLDER && (x.EndDate == null || x.EndDate > DateTime.UtcNow));
        if (sharingItem == null)
        {
          await SendErrorResult(404);
          return;
        }
        sharingDetails.Name = sharingFolder.FolderName;
        sharingDetails.End = sharingItem.EndDate;
        sharingDetails.Access = sharingItem.AccessType;
        if (sharingItem.AccessType == Models.Enums.SharedItemAccessTypesEnum.SPECIFIC_USERS)
        {
          sharingDetails.Users = GetUsers(sharingItem.Data);
        }
        await SendAsync(new SuccessDataResult<SharingDetailsResponse>(sharingDetails));
        return;
      }

    }
    private List<SharingDetailsUsersResponse>? GetUsers(string data){
      try {
        var response = new List<SharingDetailsUsersResponse>();
        var deserializedData = JsonSerializer.Deserialize<SharedItemData>(data, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        if(deserializedData != null){
          var userIds = deserializedData.Users;
          if(userIds != null && userIds.Length > 0){
            var users = _context.Users.Where(x => userIds.Any(a => a == x.UserId)).Select(x=>new SharingDetailsUsersResponse {
              Id = x.UserId,
              UserName = x.Username
            }).ToList();
            return users;
            
          }

        }
        return null;

      }catch{
        return null;
      }
    }
  }
}
