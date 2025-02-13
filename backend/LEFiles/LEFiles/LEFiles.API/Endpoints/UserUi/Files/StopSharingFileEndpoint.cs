using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Models.Enums;
using LEFiles.Services.ServiceModels.UserInterface.Files.Requests;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Files
{
  public class StopSharingFileEndpoint : BaseEndpoint<StopSharingRequest, IResult>
  {
    private AppDbContext _context;

    public StopSharingFileEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Post(ApiUrl + "my-cloud/files/{id}/stopsharing");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(StopSharingRequest req, CancellationToken ct)
    {

      var userId = User.GetUserId();
      if (userId == null)
      {
        await SendErrorResult(401);
        return;
      }
      var fileId = Route<string>("id");
      if (fileId != req.ItemId)
      {
        await SendErrorResult(400);
        return;
      }
      var file = await _context.FileItems.SingleOrDefaultAsync(x => x.FileId == req.ItemId && x.UserId == userId);
      if (file == null) {
        await SendErrorResult(404);
        return;
      }
      var sharedItems = await _context.SharedItems.Where(x => x.ItemId == req.ItemId && x.Type == SharedItemTypesEnum.FILE).ToListAsync(ct);
      if (sharedItems == null || sharedItems.Count == 0) {
        await SendErrorResult(404);
        return;
      }
      _context.RemoveRange(sharedItems);
      await _context.SaveChangesAsync();
      await SendAsync(new SuccessResult());
    }
  }
}
