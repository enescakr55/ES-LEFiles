using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Services.Contracts.Authentication;
using LEFiles.Services.ServiceModels.Authentication.Request;
using LEFiles.Services.ServiceModels.Authentication.Responses;

namespace LEFiles.API.Endpoints.UserInterface.Authentication
{
  public class UserLoginEndpoint : BaseEndpoint<UserLoginRequest,IDataResult<UserLoginResponse>>
  {
    private readonly IAuthenticationService _authenticationService;

    public UserLoginEndpoint(IAuthenticationService authenticationService)
    {
      _authenticationService = authenticationService;
    }

    public override void Configure()
    {
      Post(ApiUrl + "auth/login");
      AllowAnonymous();
    }
    public override async Task HandleAsync(UserLoginRequest req, CancellationToken ct)
    {
      var response = _authenticationService.Login(req);
      await SendAsync(response);
    }
  }
}
