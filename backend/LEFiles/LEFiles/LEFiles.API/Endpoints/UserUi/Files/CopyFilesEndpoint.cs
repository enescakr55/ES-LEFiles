using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.DataAccess;
using LEFiles.Models.Entities;
using LEFiles.Services.FileUploadProcessors.Abstract;
using LEFiles.Services.ServiceModels.UserInterface.Files.Requests;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LEFiles.API.Endpoints.UserUi.Files
{
  public class CopyFilesEndpoint : BaseEndpoint<CopyFilesRequest, IResult>
  {
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    public CopyFilesEndpoint(AppDbContext context, IConfiguration configuration)
    {
      _context = context;
      _configuration = configuration;
    }

    public override void Configure()
    {
      Post(ApiUrl + "my-cloud/files/management/copy");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(CopyFilesRequest req, CancellationToken ct)
    {
      var userId = User.GetUserId();
      if (userId == null)
      {
        await SendErrorResult(401);
        return;
      }
      var destFolderId = req.Destination;
      FolderItem? destFolder = destFolderId == null ? null : _context.FolderItems.SingleOrDefault(x => x.UserId == userId && x.FolderId == req.Destination);
      if(destFolderId != null && destFolder == null){
        await SendErrorResult(404, "Folder not found");
        return;
      }

      
      var fileIds = req.SourceFiles;
      var originalFiles = await _context.FileItems.Include(x => x.FileUploadItem).Where(x => x.UserId == userId && fileIds.Any(a => a == x.FileId)).ToListAsync();
      originalFiles.ForEach(file =>
      {
        if (file.FileUploadItem != null)
        {
          switch (file.FileUploadItem.Provider) {
            case 0:
              var localUploadPath = _configuration.GetSection("Paths:FileUpload").Value;
              if (localUploadPath == null || localUploadPath == "")
              {
                SendErrorResult(500, "File path not found");
                return;
              }
              if(file.FileUploadItem.FilePath == null){
                SendErrorResult(500, "File not found");
                return;
              }
              var fileUuid = Guid.NewGuid().ToString("N");
              var stream = new FileStream(file.FileUploadItem.FilePath, FileMode.Open);
              var newDestinationPath = $"{localUploadPath}{fileUuid}{file.FileUploadItem.Extension}";
              var destStream = new FileStream(newDestinationPath,FileMode.Create);
              stream.CopyTo(destStream);
              stream.Close();
              stream.Dispose();
              destStream.Close();
              destStream.Dispose();
              var fileUploadEntry = new FileUploadItem
              {
                CreatedAt = file.FileUploadItem.CreatedAt,
                Extension = file.FileUploadItem.Extension,
                FileName = file.FileUploadItem.FileName,
                FilePath = newDestinationPath,
                FileSize = file.FileUploadItem.FileSize,
                Id = fileUuid,
                Provider = 0,
                Status = Models.Enums.FileUploadStatus.UPLOADED,
                UploadedAt = file.FileUploadItem.UploadedAt,
                UserId = userId
              };

              _context.FileUploadItems.Add(fileUploadEntry);
              _context.SaveChanges();
              var fileEntry = new FileItem
              {
                ContentType = file.ContentType,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Extension = file.Extension,
                FileAttribute = file.FileAttribute,
                FileId = Guid.NewGuid().ToString("N"),
                FileName = file.FileName,
                FilePath = newDestinationPath,
                FileSize = file.FileSize,
                FileUploadId = fileUploadEntry.Id,
                ParentFolderId = destFolderId,
                Shared = false
              };
              _context.Add(fileEntry);
              _context.SaveChanges();
              if(destFolder != null && req.Destination != null){
                destFolder.LastUpdatedAt = DateTime.UtcNow;
                _context.Update(destFolder);
                _context.SaveChanges();
              }
              var assembly = Assembly.GetEntryAssembly();
              if (assembly != null)
              {
                var assemblies = assembly.GetReferencedAssemblies();
                foreach (var assemblyName in assemblies)
                {
                  assembly = Assembly.Load(assemblyName);

                  foreach (var ti in assembly.DefinedTypes)
                  {
                    if (ti.IsClass && ti.FullName != null && !ti.IsAbstract && ti.BaseType == typeof(FileUploadProcessorBase))
                    {
                      FileUploadProcessorBase? uploadProcessItem = assembly.CreateInstance(ti.FullName) as FileUploadProcessorBase;
                      if (uploadProcessItem != null)
                      {
                        uploadProcessItem.SetFileItem(fileEntry);
                        uploadProcessItem.Start();
                      }
                    }
                  }
                }
              }
              break;
          }
        }
      });
    }
  }
}
