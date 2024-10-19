using LEFiles.Core.Models.Results.Abstract;

namespace LEFiles.Core.Models.Results.Concrete
{
  public class ErrorResult : Result, IResult
  {
    public ErrorResult() : base(false)
    {

    }
    public ErrorResult(string message) : base(false, message)
    {

    }
  }
}
