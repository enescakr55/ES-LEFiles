using LEFiles.Client.Core.Models;
using LEFiles.Client.Core.Services.FileManagement.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.UI.Services.FileManagement.Concrete
{
    public class WindowsFileManagementService : IFileManagementService
    {
        public List<FileSystemEntries> GetFileSystemEntries(string? path = null)
        {
            if (path == null)
            {
                List<FileSystemEntries> entries = DriveInfo.GetDrives().Select(a => new FileSystemEntries
                {
                    EntryType = Core.Models.Enums.EntryTypes.Drive,
                    FullPath = a.RootDirectory.FullName,
                    Name = a.Name,
                    DriveProperties = new FileSystemEntries.DrivePropertiesItem
                    {
                        DriveType = a.DriveType.ToString(),
                        Format = a.DriveFormat,
                        FreeSpace = (long)Math.Round((decimal)a.AvailableFreeSpace / 1000000000, 2),
                        Letter = a.RootDirectory.Root.Name,
                        TotalSize = (long)Math.Round((decimal)a.TotalSize / 1000000000, 2)

                    }

                }).ToList();
                return entries;
            }
            else
            {
                var files = Directory.GetFiles(path).Select(a =>
                {
                    var fileInfo = new FileInfo(a);
                    return new FileSystemEntries
                    {
                        EntryType = Core.Models.Enums.EntryTypes.File,
                        FullPath = fileInfo.FullName,
                        Name = fileInfo.Name,
                        FileProperties = new FileSystemEntries.FilePropertiesItem
                        {
                            Extension = fileInfo.Extension,
                            Size = fileInfo.Length
                        }
                    };
                }).ToList();
                var folders = Directory.GetDirectories(path).Select(a =>
                {
                    var folderInfo = new DirectoryInfo(a);
                    return new FileSystemEntries
                    {
                        EntryType = Core.Models.Enums.EntryTypes.Folder,
                        FullPath = folderInfo.FullName,
                        Name = folderInfo.Name,


                        FolderProperties = new FileSystemEntries.FolderPropertiesItem
                        {
                            LastAccess = folderInfo.LastAccessTimeUtc,
                            LastWrite = folderInfo.LastWriteTimeUtc
                        }
                    };
                }).ToList();
                folders.AddRange(files);
                return folders;
            }
        }
    }
}
