using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Services.ServiceModels.UserInterface.Clients.Responses;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Clients
{
  public class ClientRegistrationTokenListEndpoint : BaseEndpointWithoutRequest<IDataResult<List<GetMyClientRegistrationTokensResponse>>>
  {
    private readonly AppDbContext _context;

    public ClientRegistrationTokenListEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Get(ApiUrl + "clients/registration-tokens/list");
      AuthSchemes("UserBearer", "ClientBearer");
      Roles("User");
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      var userId = User.GetUserId();
      if(userId == null){
        await SendErrorResult(401);
        return;
      }
      var tokens = await _context.ClientRegistrationTokens.Where(x => x.ExpirationDate > DateTime.UtcNow && x.UserId == userId).Select(a => new GetMyClientRegistrationTokensResponse
      {
        CreatedAt = a.CreatedAt,
        DeviceName = a.ClientName,
        Expiration = a.ExpirationDate,
        Id = a.Id,
        Secret = a.Secret,
        Token = a.Token
      }).ToListAsync();
      await SendAsync(new SuccessDataResult<List<GetMyClientRegistrationTokensResponse>>(tokens));
    }
  }
}
