using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Models.Entities;
using LEFiles.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Files
{
  public class CreateFileEntryEndpoint : BaseEndpointWithoutRequest<IDataResult<string>>
  {
    private readonly AppDbContext _context;

    public CreateFileEntryEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Get(ApiUrl + "my-cloud/upload-file");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      var folderId = Query<string>("folder", false);
      var userId = User.GetUserId();
      if(userId == null) {
        await SendErrorResult(401);
        return;
      }
      if(folderId != null){
        var folder = await _context.FolderItems.SingleOrDefaultAsync(x => x.FolderId == folderId && x.UserId == userId);
        if(folder == null){
          await SendErrorResult(404);
          return;
        }
      }
      var fileUpload = new FileUploadItem
      {
        UserId = userId,
        CreatedAt = DateTime.UtcNow,
        Id = Guid.NewGuid().ToString(),
        
        Provider = 0,
        Status = FileUploadStatus.ENTRYCREATED
      };
      var fileItem = new FileItem
      {
        FileId = Guid.NewGuid().ToString(),
        FileUploadId = fileUpload.Id,
        
        UserId = userId,
        ParentFolderId = folderId,
        Shared = false,
        CreatedAt = DateTime.UtcNow,
      };

      try {
        await _context.Database.BeginTransactionAsync();
        await _context.AddAsync(fileUpload);
        await _context.AddAsync(fileItem);
        await _context.SaveChangesAsync();
        await _context.Database.CommitTransactionAsync();
        await SendAsync(new SuccessDataResult<string>("OK",fileUpload.Id));
      }
      catch{
        await _context.Database.RollbackTransactionAsync();
        await SendErrorResult(500);
        return;
      }
    }
  }
}
