using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces.Input;

namespace WebApplication1.Hubs;

[Authorize]
public class EstimationGameHub : Hub
{
    private readonly IGameSessionService _gameSessionService;

    public EstimationGameHub(IGameSessionService gameSessionService)
    {
        _gameSessionService = gameSessionService;
    }

    public override async Task OnConnectedAsync()
    {
        string email = GetEmailFromContext(Context);
        await Clients.All.SendAsync("Receive Message", $"{email} - {Context.ConnectionId} has joined");
        
        _gameSessionService.AddNewSessionUser(email);
        
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string email = GetEmailFromContext(Context);
        _gameSessionService.RemoveSessionUser(email);
        
        await base.OnDisconnectedAsync(exception);
    }
    
    public async Task JoinSession(string sessionName)
    {
        string email = GetEmailFromContext(Context);
        

        if (_gameSessionService.IsUserInSession(email, sessionName))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionName);
        }
    }

    public async Task LeaveSession(string sessionId)
    {
        string email = GetEmailFromContext(Context);
        
        _gameSessionService.LeaveSession(email, sessionId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId); //?
        // return Task.CompletedTask;
    }

    public async Task TestMeGroup(string sessionName, string message)
    {
        //TODO: Check if user is in session.
        await Clients.Group(sessionName).SendAsync(
            "Receive Group Message",
            $"{Context.User?.Identity?.Name} - {Context.ConnectionId} send : {message}");
    }

    private string GetEmailFromContext(HubCallerContext context)
    {
        List<string> claimTypes = new List<string> { ClaimTypes.Email };
        var dict = GetClaimValues(claimTypes, context);
        
        string? email = dict.GetValueOrDefault(ClaimTypes.Email);
        if (email == null)
            throw new ArgumentException();
        
        return email;
    }

    private Dictionary<string, string> GetClaimValues(List<string> claimTypes, HubCallerContext context)
    {
        var user = context.User;
        if (user == null)
        {
            Context.Abort();
            throw new ArgumentException();
        }

        Dictionary<string, string>  dict = new Dictionary<string, string>();
        
        foreach (var claimType in claimTypes)
        {
            var claim = user.Claims.FirstOrDefault(claim => claim.Type.Equals(claimType));
            if (claim == null)
            {
                Context.Abort();
                throw new SecurityTokenArgumentException($"Token does not contain {claimType} claim");
            }
            dict.Add(claimType, claim.Value);
        }

        return dict;
    }
}