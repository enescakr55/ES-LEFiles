using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Services.Service.Users;
using LEFiles.Services.ServiceModels.Users.Requests;

namespace LEFiles.API.Endpoints.UserUi.Profile

{
  public class ChangePasswordEndpoint : BaseEndpoint<ChangePasswordRequest,IResult>
  {
    private readonly IUserManagementService _userManagementService;

    public ChangePasswordEndpoint(IUserManagementService userManagementService)
    {
      _userManagementService = userManagementService;
    }

    public override void Configure()
    {
      Post(ApiUrl + "profile/changepassword");
      Roles("User");
      AuthSchemes("UserBearer");
    }
    public override async Task HandleAsync(ChangePasswordRequest req, CancellationToken ct)
    {
      var userId = User.GetUserId();
      if (userId == null)
      {
        await SendErrorResult(401);
        return;
      }
      var result = _userManagementService.ChangePassword(req);
      await SendAsync(result);
    }
  }
}
