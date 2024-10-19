using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.CoreProject.DataAccess
{
  public class DatabaseConnectModel
  {
    public string ConnectionString { get; set; } = string.Empty;
    public string? Schema { get; set; } = null;
    public string Provider { get; set; } = string.Empty;
  }
}
