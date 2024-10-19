using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.API.Endpoints.Clients
{
  public class TestEndpoint : BaseEndpointWithoutRequest<Core.Models.Results.Abstract.IResult>
  {
    public override void Configure()
    {
      Get(ApiUrl + "test");
      AllowAnonymous();
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      await SendAsync(new SuccessResult("Endpoint is working"));
    }
  }
}
