using Domain;

namespace Services.Interfaces.Input;

public interface IGameSessionService
{
    //User Related
    public void JoinSession(string email, string sessionId);
    public void LeaveSession(string email, string sessionId);
    public bool IsUserInSession(string email, string sessionId);
    public List<GameSessionUser> GetAllUsersInSession(string sessionId);
    
    //Session Related
    public void CreateSession(string sessionId);
    public void CreateSession(string sessionId, string sessionName);
    public void CloseSession(string sessionId);
    public GameSession GetSession(string sessionId);
    public List<GameSession> GetAllActiveSessions();
    public List<GameSession> GetAllClosedSessions();
}