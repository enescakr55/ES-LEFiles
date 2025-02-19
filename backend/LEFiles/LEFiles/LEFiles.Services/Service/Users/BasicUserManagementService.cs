using Global.CoreProject.Extensions;
using LEFiles.Core.Helpers;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Services.ServiceModels.Users.Requests;
using LEFiles.Services.ServiceModels.Users.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IResult = LEFiles.Core.Models.Results.Abstract.IResult;

namespace LEFiles.Services.Service.Users
{
  public class BasicUserManagementService : IUserManagementService
  {
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;

    public BasicUserManagementService(AppDbContext context, IHttpContextAccessor contextAccessor)
    {
      _context = context;
      _contextAccessor = contextAccessor;
    }

    public IResult ChangePassword(ChangePasswordRequest request)
    {
      var userId = _contextAccessor?.HttpContext?.User.GetUserId() ?? null;
      if (string.IsNullOrEmpty(userId))
      {
        throw new HttpRequestException("", null, System.Net.HttpStatusCode.Unauthorized);
      }
      var user = _context.Users.SingleOrDefault(x => x.UserId == userId);
      if (user == null)
      {
        throw new HttpRequestException("", null, System.Net.HttpStatusCode.NotFound);
      }
      if (string.IsNullOrEmpty(request.OldPassword) || string.IsNullOrEmpty(request.NewPassword) || string.IsNullOrEmpty(request.NewPasswordAgain) || request.NewPassword != request.NewPasswordAgain)
      {
        throw new HttpRequestException("", null, HttpStatusCode.BadRequest);
      }

      //todo : password validator 

      var oldPasswordCheck = HashingHelper.VerifyPasswordHash(request.OldPassword, user.PasswordHash, user.PasswordSalt);
      if (oldPasswordCheck == false)
      {
        throw new HttpRequestException("authentication.pleaseCheckOldPassword", null, HttpStatusCode.BadRequest);
      }
      byte[] passwordHash;
      byte[] passwordSalt;
      HashingHelper.CreatePasswordHash(request.NewPassword, out passwordHash, out passwordSalt);
      user.PasswordSalt = passwordSalt;
      user.PasswordHash = passwordHash;
      _context.Update(user);
      _context.SaveChanges();
      return new SuccessResult();
    }

    public IDataResult<UserProfileDetailsResponse> GetProfileDetails()
    {
      var userId = _contextAccessor?.HttpContext?.User.GetUserId() ?? null;
      if (string.IsNullOrEmpty(userId))
      {
        throw new HttpRequestException("", null, System.Net.HttpStatusCode.Unauthorized);
      }
      var user = _context.Users.SingleOrDefault(x => x.UserId == userId);
      if (user == null)
      {
        throw new HttpRequestException("", null, System.Net.HttpStatusCode.NotFound);
      }
      var profileDetails = new UserProfileDetailsResponse
      {
        Email = user.Email,
        Firstname = user.Firstname,
        Lastname = user.Lastname,
        RegistrationDate = user.RegistrationDate,
        UserId = user.UserId
      };
      return new SuccessDataResult<UserProfileDetailsResponse>(profileDetails);

    }

    public IResult UpdateProfile(UpdateProfileRequest request)
    {
      var userId = _contextAccessor?.HttpContext?.User.GetUserId() ?? null;
      if (string.IsNullOrEmpty(userId))
      {
        throw new HttpRequestException("", null, System.Net.HttpStatusCode.Unauthorized);
      }

      var firstNameValid = Regex.IsMatch(request.Firstname, @"^[a-zA-Z üÜçÇöÖğĞşŞıİ]+$");
      var lastNameValid = Regex.IsMatch(request.Firstname, @"^[a-zA-Z üÜçÇöÖğĞşŞıİ]+$");
      var emailValid = new EmailAddressAttribute().IsValid(request.Email);

      if (firstNameValid && lastNameValid && emailValid && request.Firstname.Length > 0 && request.Firstname.Length > 0)
      {
        var currentUser = _context.Users.SingleOrDefault(x => x.UserId == userId);
        if (currentUser == null)
        {
          throw new HttpRequestException("", null, System.Net.HttpStatusCode.NotFound);
        }
        var emailExists = _context.Users.SingleOrDefault(x => x.Email.ToLower() == request.Email.Trim().ToLower() && x.UserId != currentUser.UserId);
        if (emailExists != null)
        {
          throw new HttpRequestException("profileUpdate.conflictEmailError", null, System.Net.HttpStatusCode.Conflict);
        }
        currentUser.Firstname = request.Firstname.OnlyFirstCharUpper();
        currentUser.Lastname = request.Lastname.OnlyFirstCharUpper();
        currentUser.Email = request.Email.ToLower();
        _context.Update(currentUser);
        _context.SaveChanges();
        return new SuccessResult();
      }
      else
      {
        throw new HttpRequestException("common.pleaseCheckForm", null, HttpStatusCode.BadRequest);
      }

    }
  }
}
