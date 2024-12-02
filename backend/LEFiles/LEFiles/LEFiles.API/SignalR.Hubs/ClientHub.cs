using LEFiles.Services.Contracts.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace LEFiles.API.SignalR.Hubs
{
  public class ClientHub : Hub<IClientHubServer>
  {
    
    [Authorize(AuthenticationSchemes = "ClientBearer",Roles = "Client")]
    public override Task OnConnectedAsync()
    {
      return base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception? exception)
    {
      return base.OnDisconnectedAsync(exception);
    }
    public async Task SendAction(){
      await SendAction();
    }
  }
}
