using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Services.Contracts.Authentication;
using LEFiles.Services.ServiceModels.Authentication.Request;

namespace LEFiles.API.Endpoints.UserInterface.Authentication
{
  public class UserRegistrationEndpoint : BaseEndpoint<UserRegisterRequest,IResult>
  {
    private readonly IAuthenticationService _authenticationService;
    public UserRegistrationEndpoint(IAuthenticationService authenticationService)
    {
      _authenticationService = authenticationService;
    }

    public override void Configure()
    {
      Post(ApiUrl + "auth/register");
      AllowAnonymous();
    }
    public override async Task HandleAsync(UserRegisterRequest req, CancellationToken ct)
    {
      var response = _authenticationService.Register(req);
      await SendAsync(response);
    }
  }
}
