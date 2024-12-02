using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace LEFiles.Client.Core.Helpers.Contracts
{
  public interface ISystemInformationHelper
  {
    public string? GetCPUId();
    public string GetIdentifier();
  }
}
