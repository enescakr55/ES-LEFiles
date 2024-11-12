using LEFiles.Services.Contracts.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace LEFiles.API.SignalR.Hubs
{
  public class WindowsHub : Hub<IWindowsHubServer>
  {
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
