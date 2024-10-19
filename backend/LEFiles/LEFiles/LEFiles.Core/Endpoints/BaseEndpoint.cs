using FastEndpoints;
using LEFiles.Core.Models.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Core.Endpoints
{
  public class BaseEndpoint<TRequest,TResponse> : Endpoint<TRequest,TResponse> where TRequest : notnull
  {
    protected string ApiUrl = "/api/v1/";
    protected Task SendErrorResult(int statusCode = 400,string message="", CancellationToken cancellation = default(CancellationToken))
    {
      return base.HttpContext.Response.SendAsync<ErrorResult>(new ErrorResult(message ?? ""), statusCode);
    }
  }
}
