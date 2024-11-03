
using LEFiles.Client.Core.ServiceResults.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.ServiceResults.Concrete
{
  public class SuccessDataResult<T> : DataResult<T>,IResult,IDataResult<T>
  {
    public SuccessDataResult() : base(true)
    {

    }
    public SuccessDataResult(string message) : base(true, message)
    {

    }
    public SuccessDataResult(T data) : base(true, data)
    {

    }
    public SuccessDataResult(string message, T data) : base(true, data, message)
    {

    }
  }
}
