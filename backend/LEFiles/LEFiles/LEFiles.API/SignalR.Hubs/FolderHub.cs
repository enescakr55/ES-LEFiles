using Microsoft.AspNetCore.SignalR;

namespace LEFiles.API.SignalR.Hubs
{
  public class FolderHub : Hub
  {
    public async override Task OnConnectedAsync()
    {

    }
    public async override Task OnDisconnectedAsync(Exception? exception)
    {

    }
  }
}
