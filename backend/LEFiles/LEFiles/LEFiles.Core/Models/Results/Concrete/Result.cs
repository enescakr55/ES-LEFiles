
using LEFiles.Core.Models.Results.Abstract;


namespace LEFiles.Core.Models.Results.Concrete
{
  public class Result : IResult
  {
    public bool Success { get; }

    public string Message { get; }
    public Result(bool success)
    {
      this.Success = success;
    }
    public Result(bool success, string message) : this(success)
    {
      Message = message;
    }
  }
}
