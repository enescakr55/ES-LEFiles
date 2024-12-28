using Global.CoreProject.Extensions;
using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Services.ServiceModels.UserInterface.Clients.Responses;
using Microsoft.EntityFrameworkCore;

namespace LEFiles.API.Endpoints.UserUi.Clients
{
  public class MyDevicesListEndpoint : BaseEndpointWithoutRequest<IDataResult<List<GetMyClientsResponse>>>
  {
    private readonly AppDbContext _context;

    public MyDevicesListEndpoint(AppDbContext context)
    {
      _context = context;
    }

    public override void Configure()
    {
      Get(ApiUrl + "clients/my-devices");
      AuthSchemes("UserBearer", "ClientBearer");
      Roles("User");
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
      var userId = User.GetUserId();
      if(userId == null) {
        await SendErrorResult(401);
        return;
      }
      var devices = await _context.Clients.Where(x => x.UserId == userId).Select(a => new GetMyClientsResponse
      {
        ClientName = a.ClientName,
        Description = a.Description,
        Id = a.ClientId,
        IsActive = a.IsActive,
        OperatingSystem = a.OperatingSystem
      }).ToListAsync();
      await SendAsync(new SuccessDataResult<List<GetMyClientsResponse>>(devices));
    }
  }
}
