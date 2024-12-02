using Microsoft.AspNetCore.SignalR;

namespace LEFiles.API.SignalR.Hubs
{
  public class UiHub : Hub
  {
    public override Task OnConnectedAsync()
    {
      return base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception? exception)
    {
      return base.OnDisconnectedAsync(exception);
    }
  }
}
