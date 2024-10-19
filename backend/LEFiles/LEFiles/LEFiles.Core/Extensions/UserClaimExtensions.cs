using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Global.CoreProject.Extensions
{
  public static class UserClaimExtensions
  {
    public static string? GetUserId(this ClaimsPrincipal claimsPrincipal) {
      if(claimsPrincipal != null && claimsPrincipal.Claims != null && claimsPrincipal.Claims.Count() > 0){
        var claim = claimsPrincipal.Claims.SingleOrDefault(x => x.Type == "sub") ?? claimsPrincipal.Claims.SingleOrDefault(x=>x.Type == ClaimTypes.NameIdentifier);
        if (claim != null) { return claim.Value; };
      }
      return null;
      

    }
  }
}
