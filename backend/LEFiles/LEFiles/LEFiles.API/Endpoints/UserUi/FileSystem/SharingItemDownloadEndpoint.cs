using LEFiles.Core.Endpoints;
using LEFiles.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.FileSystem
{
  public class SharingItemDownloadEndpoint : BaseEndpointWithoutRequest<object>
  {
    private readonly AppDbContext _context;

    public SharingItemDownloadEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Get(ApiUrl + "shared/{key}/download");
      AllowAnonymous();
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      var sharedKey = Route<string>("key");
      var accessToken = Query<string>("token");
      if(string.IsNullOrEmpty(sharedKey) || string.IsNullOrEmpty(accessToken)){
        await SendErrorResult(400);
        return;
      }
      var sharedItem = await _context.SharedItems.SingleOrDefaultAsync(x => x.AccessKey == sharedKey && (x.EndDate == null || x.EndDate > DateTime.UtcNow));
      if(sharedItem == null){
        await SendErrorResult(404);
        return;
      }
      var fileItem = await _context.FileItems.SingleOrDefaultAsync(x => x.FileId == sharedItem.ItemId);
      if(fileItem == null){
        await SendErrorResult(404);
        return;
      }
      var tokenItem = await _context.EntryAccessItems.SingleOrDefaultAsync(x => x.Expiration > DateTime.UtcNow && x.Code == accessToken && x.EntryType == Models.Enums.EntryTypesEnum.SHARED_FILE && x.Target == fileItem.FileId && x.SubTarget == sharedItem.Id);
      if(tokenItem == null){
        await SendErrorResult(403);
        return;
      }
      try {
        using(var fileStream = new FileStream(fileItem.FilePath, FileMode.Open)){
          await SendStreamAsync(fileStream, fileItem.FileName);
        };
      }catch{
        await SendErrorResult(500);
        return;
      }


    }
  }
}
