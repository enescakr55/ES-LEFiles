using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.Models
{
  public class AuthenticationInfoModel
  {
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string Username { get; set; }
  }
}
