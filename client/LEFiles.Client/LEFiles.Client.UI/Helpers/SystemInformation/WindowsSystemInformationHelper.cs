using LEFiles.Client.Core.Helpers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.UI.Helpers.SystemInformation
{
  public class WindowsSystemInformationHelper : ISystemInformationHelper
  {
    public string? GetCPUId()
    {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        ManagementClass managementClass = new ManagementClass("win32_processor");
        if (managementClass != null)
        {
          ManagementObjectCollection collection = managementClass.GetInstances();
          foreach (ManagementObject managObj in collection)
          {
            return managObj.Properties["processorID"].Value.ToString();
          }
        }
        return null;
      }
      else
      {
        throw new Exception("Unsupported OS");
      }

    }

    public string GetIdentifier()
    {
      return GetCPUId() ?? "";
    }
    public string? GetHddSerial()
    {
      ManagementObjectSearcher searcher;

      searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
      string? serial_number = null;

      foreach (ManagementObject wmi_HD in searcher.Get())
      {

        serial_number = wmi_HD["SerialNumber"] != null ? wmi_HD["SerialNumber"].ToString() : null;
        if (serial_number != null)
        {
          return serial_number;
        }
      }
      return null;
    }
  }
}
