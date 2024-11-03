using LEFiles.Client.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.Services.FileManagement.Abstract
{
    public interface IFileManagementService
    {
        List<FileSystemEntries> GetFileSystemEntries(string? path = null);
    }
}
