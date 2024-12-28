using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Models.Entities;
using LEFiles.Services.ServiceModels.UserInterface.Folders.Requests;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Folders
{
  public class CreateFolderEndpoint : BaseEndpoint<CreateFolderRequest,IDataResult<string>>
  {
    private readonly AppDbContext _context;

    public CreateFolderEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Post(ApiUrl + "my-cloud/folders/create");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(CreateFolderRequest req, CancellationToken ct)
    {
      var userId = User.GetUserId();
      if(userId == null){
        await SendErrorResult(401);
        return;
      }
      var folderExists = await _context.FolderItems.SingleOrDefaultAsync(x => x.FolderName.ToLower() == req.FolderName.ToLower() && x.ParentFolderId == req.ParentFolder);
      if(folderExists != null){
        await SendErrorResult(409, "Folder already exists");
        return;
      }
      var folderItem = new FolderItem
      {
        CreatedAt = DateTime.UtcNow,
        FolderId = Guid.NewGuid().ToString("N"),
        FolderName = req.FolderName,
        ParentFolderId = req.ParentFolder,
        Shared = req.Shared,
        UserId = userId
      };
      var result = await _context.AddAsync(folderItem);
      await _context.SaveChangesAsync();
      await SendAsync(new SuccessDataResult<string>(result.Entity.FolderId));
    }
  }
}
