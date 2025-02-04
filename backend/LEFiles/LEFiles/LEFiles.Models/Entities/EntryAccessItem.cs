using LEFiles.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Entities
{
  public class EntryAccessItem
  {
    public int Id { get; set; }
    public EntryTypesEnum EntryType { get; set; }
    public string Target { get; set; } = string.Empty;
    public string? SubTarget { get; set; } //if shared
    public string? UserId { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime Expiration { get; set; }
    public User? User { get; set; }

  }
}
