
using LEFiles.Core.Models.Results.Abstract;


namespace LEFiles.Core.Models.Results.Concrete
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
