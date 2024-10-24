using LEFiles.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Entities
{
  public class WaitableRequest
  {
    public string RequestId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string ClientId { get;set; } = string.Empty;
    public WaitableRequestTypes RequestType { get; set; }
    public string RequestContent { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
    public DateTime RequestDate { get; set; }
    public DateTime? ResponseDate { get; set; }


  }
}
