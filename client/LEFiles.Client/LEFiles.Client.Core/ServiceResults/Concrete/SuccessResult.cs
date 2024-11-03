
using LEFiles.Client.Core.ServiceResults.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.ServiceResults.Concrete
{
  public class SuccessResult : Result,IResult
  {
    public SuccessResult() : base(true)
    {

    }
    public SuccessResult(string message) : base(true, message)
    {

    }
  }
}
