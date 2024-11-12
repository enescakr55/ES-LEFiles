using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.Contracts.Hubs
{
  public interface IWindowsHubServer
  {
    Task FileSystemViewRequest();
    Task SendAction();
  }
}
