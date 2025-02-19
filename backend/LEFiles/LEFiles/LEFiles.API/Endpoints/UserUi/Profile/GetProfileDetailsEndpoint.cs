using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Services.Service.Users;
using LEFiles.Services.ServiceModels.Users.Responses;

namespace LEFiles.API.Endpoints.UserUi.Profile
{
  public class GetProfileDetailsEndpoint : BaseEndpointWithoutRequest<IDataResult<UserProfileDetailsResponse>>
  {
    private readonly IUserManagementService _userManagementService;

    public GetProfileDetailsEndpoint(IUserManagementService userManagementService)
    {
      _userManagementService = userManagementService;
    }

    public override void Configure()
    {
      Get(ApiUrl + "profile/details");
      Roles("User");
      AuthSchemes("UserBearer");
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      var userId = User.GetUserId();
      if (userId == null)
      {
        await SendErrorResult(401);
        return;
      }
      var details = _userManagementService.GetProfileDetails();
      await SendAsync(details);
    }
  }
}
