using Global.CoreProject.Extensions;
using LEFiles.Common.Constraints;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Models.Entities;
using LEFiles.Services.ServiceModels.UserInterface.Folders.Responses;
using LEFiles.Services.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;

namespace LEFiles.API.Endpoints.UserUi.Folders
{
  public class FileAndFoldersListEndpoint : BaseEndpointWithoutRequest<IDataResult<FileAndFoldersResponse>>
  {
    private readonly AppDbContext _context;

    public FileAndFoldersListEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Get(ApiUrl + "my-cloud/view-files");
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
      var response = new FileAndFoldersResponse();
      var parents = new List<ParentFolderEntryResponse>();
      var folderId = Query<string?>("parentId", false);
      var filters = Query<string?>("filters", false);
      var viewType = Query<string?>("viewType", false);
      //Do while
      FolderItem? folderItem = _context.FolderItems.SingleOrDefault(x => x.FolderId == folderId);
      if (folderId != null)
      {
        do
        {
          if (folderItem == null)
          {
            break;
          }
          parents.Add(new ParentFolderEntryResponse
          {
            Id = folderItem.FolderId,
            Name = folderItem.FolderName
          });
          if (folderItem.ParentFolderId != null)
          {
            folderItem = _context.FolderItems.SingleOrDefault(x => x.FolderId == folderItem.ParentFolderId);
          }
          else
          {
            folderItem = null;
          }

        } while (folderItem != null);
      }
      var folders = await _context.FolderItems.Where(x => x.UserId == userId && x.ParentFolderId == folderId).Select(a => new FileSystemEntryResponse
      {
        Id = a.FolderId,
        Name = a.FolderName,
        Shared = a.Shared,
        CreatedAt = a.CreatedAt,
        Extension = null,
        Type = 0
      }).ToListAsync();
      var filesQuery = _context.FileItems.Include(a => a.FileUploadItem).Where(x => x.UserId == userId && x.FileUploadItem.Status == Models.Enums.FileUploadStatus.UPLOADED);
      if (viewType == null || viewType == "h")
      {
        filesQuery = filesQuery.Where(x => x.ParentFolderId == folderId);
      }
      List<string> filterList = new();
      if (filters != null)
      {
        var filterItems = filters.Split(",");
        if (filterItems.Length > 0)
        {
          if (filterItems.FirstOrDefault(x => x == "images") != null)
          {
            filterList.AddRange(FileExtensionTypes.Images);
          }
          if (filterItems.FirstOrDefault(x => x == "audios") != null)
          {
            filterList.AddRange(FileExtensionTypes.Audios);
          }
          if (filterItems.FirstOrDefault(x => x == "videos") != null)
          {
            filterList.AddRange(FileExtensionTypes.Videos);
          }
          if (filterItems.FirstOrDefault(x => x == "documents") != null)
          {
            filterList.AddRange(FileExtensionTypes.Documents);
          }
        }
        filesQuery = filesQuery.Where(x => filterList.Contains(x.Extension.ToLower()));
      }
      var files = await filesQuery.Select(a => new FileSystemEntryResponse
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
      }).ToListAsync();

      List<FileSystemEntryResponse> fileAndFolders = new List<FileSystemEntryResponse>();
      if (viewType == null || viewType == "h")
      {
        fileAndFolders.AddRange(folders);
      }
      fileAndFolders.AddRange(files);
      fileAndFolders = fileAndFolders.OrderBy(x => x.CreatedAt).ToList();
      response.Entries = fileAndFolders ?? new();
      parents.Reverse();
      response.Parents = parents;

      await SendAsync(new SuccessDataResult<FileAndFoldersResponse>(response));

    }
  }
}
