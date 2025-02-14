using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Models.Entities;
using LEFiles.Services.ServiceModels.UserInterface.Files.Requests;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Files
{
  public class MoveFilesEndpoint : BaseEndpoint<MoveFilesRequest,IResult>
  {
    private readonly AppDbContext _context;

    public MoveFilesEndpoint(AppDbContext context)
    {
      _context = context;
    }
    public override void Configure()
    {
      Post(ApiUrl + "my-cloud/files/management/move");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(MoveFilesRequest req, CancellationToken ct)
    {
      var userId = User.GetUserId();
      if(userId == null) {
        await SendErrorResult(401);
        return;
      }
      var folderId = req.Destination;
      FolderItem? folder = null;
      if(folderId != null){
        folder = _context.FolderItems.SingleOrDefault(x => x.FolderId == folderId && x.UserId == userId);
        if(folder == null){
          await SendErrorResult(404, "Destination folder not found");
          return;
        }
      }
      var fileIds = req.SourceFiles.ToList();
      var files = await _context.FileItems.Where(x => fileIds.Any(a => a == x.FileId) && x.UserId == userId).ToListAsync();
      files.ForEach(file =>
      {
        file.ParentFolderId = folderId;
      });
      if(files.Count > 0 && folderId != null && folder != null){
        folder.LastUpdatedAt = DateTime.UtcNow;
        _context.Update(folder);
      }
      _context.UpdateRange(files);
      await _context.SaveChangesAsync();
      await SendAsync(new SuccessResult());
    }
  }
}
