using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Models.Configuration;
using LEFiles.Models.Entities;
using LEFiles.Models.Enums;
using LEFiles.Services.ServiceModels.UserInterface.Files.Requests;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace LEFiles.API.Endpoints.UserUi.Files
{
  public class ShareFileEndpoint : BaseEndpoint<ShareFileRequest,IResult>
  {
    private readonly AppDbContext _context;

    public ShareFileEndpoint(AppDbContext context)
    {
      _context = context;
    }
    public override void Configure()
    {
      Post(ApiUrl + "my-cloud/files/{id}/share");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(ShareFileRequest req, CancellationToken ct)
    {
      var userId = User.GetUserId();
      if (userId == null) {
        await SendErrorResult(401);
        return;
      }
      var id = Route<string>("id");
      if(id != req.FileId){
        await SendErrorResult(400);
        return;
      }
      var file = _context.FileItems.SingleOrDefault(x => x.FileId == id && x.UserId == userId);
      if (file == null)
      {
        await SendErrorResult(404);
        return;
      }
      var sharingItems = _context.SharedItems.Where(x => x.ItemId == file.FileId && x.Type == SharedItemTypesEnum.FILE && (x.EndDate > DateTime.UtcNow) || x.EndDate == null).Count();
      
      if(file.Shared == true && sharingItems > 0){
        await SendErrorResult(409, "File already sharing");
        return;
      }
      var accessGuid = Guid.NewGuid().ToString("N");
      var accessKey = $"{Regex.Replace(file.FileName, @"[^A-Za-z_-]+", String.Empty)}_{accessGuid.Substring(2, 8)}";
      try
      {
        await _context.Database.BeginTransactionAsync();
        var shareModel = new SharedItem
        {
          AccessKey = accessKey ?? accessGuid,
          AccessType = req.Access,
          CreatedAt = DateTime.UtcNow,
          Data = CreateSharedItemData(req),
          EndDate = req.End,
          Id = Guid.NewGuid().ToString("N"),
          ItemId = file.FileId,
          Password = null,
          Type = SharedItemTypesEnum.FILE
        };
        await _context.AddAsync(shareModel);
        await _context.SaveChangesAsync();

        file.Shared = true;
        _context.Update(file);
        await _context.SaveChangesAsync();
        await _context.Database.CommitTransactionAsync();
        await SendAsync(new SuccessResult());
        return;
      }
      catch (Exception ex)
      {
        await _context.Database.RollbackTransactionAsync();
        await SendErrorResult(500);
        return;
      }

    }
    private string CreateSharedItemData(ShareFileRequest req)
    {
      var sharedItemData = new SharedItemData();
      if(req.Users != null){
        try {
          var splitList = req.Users.Split(",");
          var usernames = splitList.Select(s => s.Trim().ToLowerInvariant());
          var dbUsers = _context.Users.Where(x => usernames.Any(a => a == x.Username.ToLower())).Select(x => x.UserId).ToList();
          sharedItemData.Users = dbUsers.ToArray();
          var sharedItemDataString = JsonSerializer.Serialize(sharedItemData, typeof(SharedItemData), new JsonSerializerOptions
          {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
          });
          return sharedItemDataString;
        }
        catch{
          return "{}";
        }

      }
      return "{}";

    }
  }
}
