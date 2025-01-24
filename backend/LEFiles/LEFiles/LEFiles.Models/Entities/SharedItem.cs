using LEFiles.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Entities
{
  public class SharedItem
  {
    public string Id { get; set; } = string.Empty;
    public SharedItemTypesEnum Type { get; set; }
    public string ItemId { get; set; } = string.Empty;
    public SharedItemAccessTypesEnum AccessType { get; set; }
    public string AccessKey { get; set; } = string.Empty;
    public string? Password { get; set; } = null;
    public DateTime CreatedAt { get; set; }
    public DateTime? EndDate { get; set; }
    public string Data { get; set; } = "{}";

  }
}
