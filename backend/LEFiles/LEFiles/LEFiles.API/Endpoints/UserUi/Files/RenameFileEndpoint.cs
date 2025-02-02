using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Services.ServiceModels.UserInterface.Files.Requests;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Files
{
  public class RenameFileEndpoint : BaseEndpoint<RenameFileRequest,IResult>
  {
    private readonly AppDbContext _context;

    public RenameFileEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Post(ApiUrl + "my-cloud/files/{id}/rename");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(RenameFileRequest req, CancellationToken ct)
    {
      var userId = User.GetUserId();
      if(userId == null){
        await SendErrorResult(401);
        return;
      }
      var fileId = Route<string>("id");
      if(fileId != req.FileId){
        await SendErrorResult(400);
        return;
      }
      var file = await _context.FileItems.Include(x=>x.FileUploadItem).SingleOrDefaultAsync(x => x.FileId == req.FileId && x.UserId == userId,ct);
      if(file == null){
        await SendErrorResult(404);
        return;
      }
      var fileUploadItem = file.FileUploadItem;
      if (fileUploadItem == null)
      {
        await SendErrorResult(404);
        return;
      }
      fileUploadItem.FileName = req.FileName;
      file.FileName = req.FileName;
      _context.Update(file);
      _context.Update(fileUploadItem);
      await _context.SaveChangesAsync(ct);
      await _context.SaveChangesAsync(ct);
      await SendAsync(new SuccessResult("cloudManagement.files.fileRenamedSuccessfully"));
    }
  }
}
