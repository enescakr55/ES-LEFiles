
using LEFiles.Core.Models.Results.Abstract;


namespace LEFiles.Core.Models.Results.Concrete
{
  public class ErrorDataResult<T> : DataResult<T>, IResult, IDataResult<T>
  {

    public ErrorDataResult() : base(false)
    {

    }
    public ErrorDataResult(string message) : base(false, message)
    {

    }
    public ErrorDataResult(T data) : base(false, data)
    {

    }
    public ErrorDataResult(string message, T data) : base(false, data, message)
    {

    }



  }
}
