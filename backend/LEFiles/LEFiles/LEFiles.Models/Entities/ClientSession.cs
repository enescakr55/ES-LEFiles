using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Entities
{
  public class ClientSession
  {
    public string ClientSessionId { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string SessionCode { get; set; } = string.Empty;
    public bool Active { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; } = null;
    public virtual Client? Client { get; set; }
  }
}
