using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Services.ServiceModels.Authentication.Request;
using LEFiles.Services.ServiceModels.Authentication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.Contracts.Authentication
{
  public interface IAuthenticationService
  {
    IResult Register(UserRegisterRequest registerRequest);
    IDataResult<UserLoginResponse> Login(UserLoginRequest loginRequest);

  }
}
