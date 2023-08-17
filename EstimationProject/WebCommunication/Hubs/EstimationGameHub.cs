using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hubs;

[Authorize]
public class EstimationGameHub : Hub
{
    // private static readonly Dictionary<string, EstimationGameHub> ActiveHubs =
    //     new Dictionary<string, EstimationGameHub>();
    // private List<string> ActiveGroups

    public override async Task OnConnectedAsync()
    {
        // Console.WriteLine($"{this.Context.ConnectionId} Connected!");
        await Clients.All.SendAsync("Receive Message", $"{Context.ConnectionId} has joined");
        await Clients.Client("fdf").SendAsync("tt", "fdf");


        // await Clients.All.SendAsync("Receive Message", $"{Clients.Groups("test")}");
        // return base.OnConnectedAsync();
    }

    public async Task CreateSession(string sessionName)
    {
        
    }

    public async Task JoinSession(string sessionName)
    {
        
    }

    public async Task LeaveSession(string sessionName)
    {
        
    }

    public async Task EndSession(string sessionName)
    {
        
    }
    
    public async Task TestMe(string someRandomText)
    {
        await Clients.All.SendAsync("Receive Message",
            $"{Context.User.Identity.Name} - {Context.ConnectionId} send : {someRandomText}",
            CancellationToken.None);
            
    }
}