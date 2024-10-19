
using LEFiles.Core.Models.Results.Abstract;


namespace LEFiles.Core.Models.Results.Concrete
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
