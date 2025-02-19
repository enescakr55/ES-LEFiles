using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Services.Service.Users;
using LEFiles.Services.ServiceModels.Users.Requests;

namespace LEFiles.API.Endpoints.UserUi.Profile
{
  public class UpdateProfileEndpoint : BaseEndpoint<UpdateProfileRequest,IResult>
  {
    private readonly IUserManagementService _userManagementService;

    public UpdateProfileEndpoint(IUserManagementService userManagementService)
    {
      _userManagementService = userManagementService;
    }

    public override void Configure()
    {
      Post(ApiUrl + "profile/update");
      AuthSchemes("UserBearer");
      Roles("User");
    }
    public override async Task HandleAsync(UpdateProfileRequest req, CancellationToken ct)
    {
      var userId = User.GetUserId();
      if (userId == null)
      {
        await SendErrorResult(401);
        return;
      }
      var result = _userManagementService.UpdateProfile(req);
      await SendAsync(result);
    }
  }
}
