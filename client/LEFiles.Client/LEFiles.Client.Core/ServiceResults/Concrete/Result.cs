
using LEFiles.Client.Core.ServiceResults.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.ServiceResults.Concrete
{
  public class Result : IResult
  {
    public bool Success { get; }

    public string Message { get; } = string.Empty;
    public Result(bool success) : base()
    {
      this.Success = success;
    }
    public Result()
    {

    }
    public Result(bool success, string message) : this(success)
    {
      Message = message;
    }
  }
}
