using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Services.ServiceModels.Users.Requests;
using LEFiles.Services.ServiceModels.Users.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.Service.Users
{
  public interface IUserManagementService
  {
    public IDataResult<UserProfileDetailsResponse> GetProfileDetails();
    public IResult UpdateProfile(UpdateProfileRequest request);
    public IResult ChangePassword(ChangePasswordRequest request);
  }
}
