
using LEFiles.Core.Models.Results.Abstract;

namespace LEFiles.Core.Models.Results.Concrete
{
  public class DataResult<T> : Result, IDataResult<T>, IResult
  {
    public T Data { get; }
    public DataResult(bool success, T data) : base(success)
    {
      this.Data = data;
    }
    public DataResult(bool success, T data, string message) : base(success, message)
    {
      this.Data = data;
    }
    public DataResult(bool success) : base(success)
    {
    }
    public DataResult(bool success, string message) : base(success, message)
    {

    }
  }
}
