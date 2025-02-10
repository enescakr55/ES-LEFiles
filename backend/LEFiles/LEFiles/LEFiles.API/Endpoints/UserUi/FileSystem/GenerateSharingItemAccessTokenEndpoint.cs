using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Models.Configuration;
using LEFiles.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace LEFiles.API.Endpoints.UserUi.FileSystem
{
  public class GenerateSharingItemAccessTokenEndpoint : BaseEndpointWithoutRequest<IDataResult<string>>
  {
    private readonly AppDbContext _context;

    public GenerateSharingItemAccessTokenEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Get(ApiUrl + "shared/{key}/token");
      AllowAnonymous();
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      string? accessToken = null;
      var userId = User.GetUserId();
      var accessKey = Route<string>("key");
      if (accessKey == null)
      {
        await SendErrorResult(400);
        return;
      }
      var sharedItem = await _context.SharedItems.SingleOrDefaultAsync(x => x.AccessKey == accessKey && x.EndDate > DateTime.UtcNow);
      if (sharedItem == null)
      {
        await SendErrorResult(404);
        return;
      }
      bool accessGranted = false;
      var accessType = sharedItem.AccessType;
      switch (accessType)
      {
        case Models.Enums.SharedItemAccessTypesEnum.ANONYMOUS:
          accessGranted = true;
          break;
        case Models.Enums.SharedItemAccessTypesEnum.ALL_USERS:
          if (!string.IsNullOrEmpty(userId))
          {
            accessGranted = true;
          }
          break;
        case Models.Enums.SharedItemAccessTypesEnum.SPECIFIC_USERS:
          try
          {
            var data = JsonSerializer.Deserialize<SharedItemData>(sharedItem.Data, new JsonSerializerOptions
            {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            if (!string.IsNullOrEmpty(userId) && data != null)
            {
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
              accessGranted = grantedUsers.Any(x => x.ToLowerInvariant() == currentUser.Username.ToLowerInvariant());
            }

          }
          catch
          {
            await SendErrorResult(500);
            return;
          }
          break;
      }
      if(!accessGranted){
        await SendErrorResult(403);
        return;
      }
      accessToken = GenerateToken(30);
      var entryAccessItem = new EntryAccessItem
      {
        Code = accessToken,
        CreatedAt = DateTime.UtcNow,
        EntryType = sharedItem.Type == Models.Enums.SharedItemTypesEnum.FOLDER ? Models.Enums.EntryTypesEnum.SHARED_FOLDER : Models.Enums.EntryTypesEnum.SHARED_FILE,
        Expiration = DateTime.UtcNow.AddMinutes(15),
        UserId = userId,
        Target = sharedItem.ItemId,
        SubTarget = sharedItem.Id,
      };
      await _context.AddAsync(entryAccessItem);
      await _context.SaveChangesAsync();
      await SendAsync(new SuccessDataResult<string>("sharedItem.tokenGeneratedSuccessfully",accessToken));
    }
    private string GenerateToken(int count)
    {
      return Convert.ToBase64String(RandomNumberGenerator.GetBytes(count));
    }
  }
}
