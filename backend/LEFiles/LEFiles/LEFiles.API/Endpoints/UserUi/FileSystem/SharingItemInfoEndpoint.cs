using Global.CoreProject.Extensions;
using LEFiles.API.JwtTokenValidator;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Models.Configuration;
using LEFiles.Services.ServiceModels.UserInterface.FileSystem.Responses;
using LEFiles.Services.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace LEFiles.API.Endpoints.UserUi.FileSystem
{
  [HttpGet(ApiUrl+"shared/{key}/info")]
  [AllowAnonymous]

  public class SharingItemInfoEndpoint : BaseEndpointWithoutRequest<IDataResult<SharingItemInfoResponse>>
  {
    private readonly AppDbContext _context;
    private readonly JwtValidationService _jwtValidationService;

    public SharingItemInfoEndpoint(AppDbContext context, JwtValidationService jwtValidationService)
    {
      _context = context;
      _jwtValidationService = jwtValidationService;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
      var userId = _jwtValidationService.GetUserId();
      var key = Route<string>("key");
      var sharedItem = await _context.SharedItems.SingleOrDefaultAsync(x => x.AccessKey == key && (x.EndDate == null || x.EndDate > DateTime.UtcNow));

      if (sharedItem == null)
      {
        await SendErrorResult(404);
        return;
      }
      if (sharedItem.Type == Models.Enums.SharedItemTypesEnum.FOLDER)
      {
        await SendErrorResult(404, "Folder sharing is not availabe");
        return;
      }

      var sharedItemAccess = sharedItem.AccessType;
      if (sharedItemAccess == Models.Enums.SharedItemAccessTypesEnum.ALL_USERS && string.IsNullOrEmpty(userId))
      {
        await SendErrorResult(401, "sharedItem.allowedOnlyLoggedUsers");
        return;
      }
      if (sharedItemAccess == Models.Enums.SharedItemAccessTypesEnum.SPECIFIC_USERS)
      {
        try
        {
          if (string.IsNullOrEmpty(userId))
          {
            await SendErrorResult(401);
            return;
          }
          var data = JsonSerializer.Deserialize<SharedItemData>(sharedItem.Data, new JsonSerializerOptions
          {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
          });
          if (data == null)
          {
            await SendErrorResult(403);
            return;
          }
          var grantedUsers = data.Users;
          if (grantedUsers == null)
          {
            await SendErrorResult(403);
            return;
          }
          var currentUser = await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);
          if (currentUser == null)
          {
            await SendErrorResult(401);
            return;
          }
          if(!grantedUsers.Any(x=>x == currentUser.UserId)){
            await SendErrorResult(403,"sharedItem.fileViewSpecificUsersMessage");
            return;
          }
        }
        catch
        {
          await SendErrorResult(500);
          return;
        }
      }
      var itemInfo = new SharingItemInfoResponse();
      if(sharedItem.Type == Models.Enums.SharedItemTypesEnum.FILE){
        var file = await _context.FileItems.SingleOrDefaultAsync(x => x.FileId == sharedItem.ItemId);
        if(file == null){
          await SendErrorResult(404);
          return;
        }
        itemInfo.Name = file.FileName;
        itemInfo.Extension = file.Extension;
        itemInfo.Type = Models.Enums.SharedItemTypesEnum.FILE;
        itemInfo.Size = file.FileSize;
        itemInfo.Icon = FileTools.GetIcon(file.Extension);
        await SendAsync(new SuccessDataResult<SharingItemInfoResponse>(itemInfo));
        return;
      }
      await SendErrorResult(500);
      return;
      
    }
  }
}
