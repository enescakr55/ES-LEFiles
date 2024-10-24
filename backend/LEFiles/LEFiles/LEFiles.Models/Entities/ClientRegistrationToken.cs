using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Entities
{
  public class ClientRegistrationToken
  {
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpirationDate { get; set; }
    public virtual User? User { get; set; }
  }
}
