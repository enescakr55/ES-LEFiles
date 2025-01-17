using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Services.ServiceModels.UserInterface.Folders.Responses;
using LEFiles.Services.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LEFiles.API.Endpoints.UserUi.Folders
{
  public class SearchFilesystemEndpoint : BaseEndpointWithoutRequest<IDataResult<FileSystemEntrySearchResult>>
  {
    private readonly AppDbContext _context;

    public SearchFilesystemEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Get(ApiUrl + "my-cloud/filesystem/search");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      var userId = User.GetUserId();
      if (userId == null)
      {
        await SendErrorResult(401);
        return;
      }
      var searchParams = Query<string>("q");
      if(searchParams == null){
        await SendErrorResult(400);
        return;
      }
      if (searchParams.Length == 0) {
        await SendErrorResult(400);
        return;
      }
      var files = await _context.FileItems.Where(x => EF.Functions.Like(x.FileName, "%" + searchParams + "%") && x.UserId == userId).ToListAsync();
      var folders = await _context.FolderItems.Where(x => EF.Functions.Like(x.FolderName, "%" + searchParams + "%") && x.UserId == userId).ToListAsync();

      var folderResults = folders.Select(a => new FileSystemEntryResponse
      {
        Id = a.FolderId,
        Name = a.FolderName,
        Shared = a.Shared,
        CreatedAt = a.CreatedAt,
        LastUpdatedAt = a.LastUpdatedAt,
        Extension = null,
        Type = 0
      }).ToList();
      var fileResults = files.Select(a => new FileSystemEntryResponse
      {
        Id = a.FileId,
        Name = a.FileName,
        Type = 1,
        CreatedAt = a.CreatedAt,
        AllowPreview = FileTools.IsAllowPreview(a.Extension),
        Extension = a.Extension,
        Icon = FileTools.GetIcon(a.Extension),
        Shared = a.Shared,
        ThumbnailExists = FileTools.IsThumbnailExists(a.FileId) //Thumbnaillerin başka yerlerde depolanma olasılığına karşı servis oluşturulacak.
      }).ToList();
      var fileSystemSearchResult = new List<FileSystemEntryResponse>();
      fileSystemSearchResult.AddRange(folderResults);
      fileSystemSearchResult.AddRange(fileResults);
      fileSystemSearchResult = fileSystemSearchResult.OrderBy(x => x.CreatedAt).ToList();
      var response = new FileSystemEntrySearchResult
      {
        SearchText = searchParams,
        Result = fileSystemSearchResult
      };
      await SendAsync(new SuccessDataResult<FileSystemEntrySearchResult>(response));
    }
  }
}
