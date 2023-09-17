using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hubs;

[Authorize]
public class EstimationGameHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("Receive Message", $"{Context.ConnectionId} has joined");
        await Clients.Client("fdf").SendAsync("tt", "fdf");
        
        await base.OnConnectedAsync();
    }
    
    public async Task JoinSession(string sessionName)
    {
        
    }

    public async Task LeaveSession(string sessionName)
    {
        
    }
    
    public async Task TestMe(string someRandomText)
    {
        await Clients.All.SendAsync("Receive Message",
            $"{Context.User.Identity.Name} - {Context.ConnectionId} send : {someRandomText}",
            CancellationToken.None);
            
    }
}