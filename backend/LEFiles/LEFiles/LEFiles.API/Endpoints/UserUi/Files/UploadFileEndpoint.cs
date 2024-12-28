using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Files
{
  public class UploadFileEndpoint : BaseEndpointWithoutRequest<IResult>
  {
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public UploadFileEndpoint(AppDbContext context, IConfiguration configuration)
    {
      _context = context;
      _configuration = configuration;
    }

    public override void Configure()
    {
      Post(ApiUrl + "my-cloud/file-entry/{id}/upload");
      AuthSchemes("UserBearer");
      Roles("User");
      AllowFileUploads(dontAutoBindFormData: true);
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      var fileUploadId = Route<string>("id");
      var filename = Query<string>("filename", false) ?? "";
      var end = Query<bool>("end", false);
      var begin = Query<bool>("begin", false);
      var part = Query<int?>("part",false);
      var userId = User.GetUserId();
      if(userId == null) {
        await SendErrorResult(401);
        return;
      }
      var filesPath = _configuration.GetSection("Paths:FileUpload").Value;
      if(filesPath == null || filesPath == ""){
        await SendErrorResult(500, "File path not found");
        return;
      }

      Directory.CreateDirectory(filesPath);

      var fileUploadItem = await _context.FileUploadItems.SingleOrDefaultAsync(x => x.Id == fileUploadId && x.UserId == userId && x.Provider == 0 && x.Status != Models.Enums.FileUploadStatus.UPLOADED);
      if(fileUploadItem == null) {
        await SendErrorResult(404);
        return;
      }

      //ileride part numarası eklenecek, yüklemeye sonradan devam edilebilecek.
      switch (fileUploadItem.Status) {
        case Models.Enums.FileUploadStatus.ENTRYCREATED:
          fileUploadItem.Status = Models.Enums.FileUploadStatus.UPLOADING;
          fileUploadItem.FileName = filename;

          fileUploadItem.Extension = Path.GetExtension(filename);
          fileUploadItem.FilePath = filesPath + fileUploadItem.Id + fileUploadItem.Extension;
          await foreach (var section in FormFileSectionsAsync(ct))
          {
            if (section is not null)
            {
              using (var fs = new FileStream($"{fileUploadItem.FilePath}", FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
              {
                await section.Section.Body.CopyToAsync(fs, 1024 * 64, ct);
              }
            }
          }

          break;
        case Models.Enums.FileUploadStatus.UPLOADING:
          await foreach (var section in FormFileSectionsAsync(ct))
          {
            if (section is not null)
            {
              using (var fs = new FileStream($"{fileUploadItem.FilePath}", FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
              {
                await section.Section.Body.CopyToAsync(fs, 1024 * 64, ct);
              }
            }
          }
          break;
        case Models.Enums.FileUploadStatus.CANCELLED:
          break;
        default:
          await SendErrorResult(400);
          return;
      }
      if(end == true){
        fileUploadItem.Status = Models.Enums.FileUploadStatus.UPLOADED;
        var fileItem = await _context.FileItems.SingleOrDefaultAsync(x => x.FileUploadId == fileUploadItem.Id);
        if(fileItem == null){
          await SendErrorResult(404);
          return;
        }
        fileItem.Extension = fileUploadItem.Extension ?? "";
        fileItem.FileName = fileUploadItem.FileName ?? "undefined";
        if(fileUploadItem.FilePath != null){
          var fileInfo = new FileInfo(fileUploadItem.FilePath);
          fileItem.FileSize = (fileInfo.Length / (1024*1024))+1;
          fileUploadItem.FileSize = fileInfo.Length;
        }
        _context.Update(fileItem);
      }

      _context.FileUploadItems.Update(fileUploadItem);
      await _context.SaveChangesAsync();
      await SendAsync(new SuccessResult());
    }
  }
}
