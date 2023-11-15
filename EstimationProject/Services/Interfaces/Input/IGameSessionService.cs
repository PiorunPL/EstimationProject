using Domain;

namespace Services.Interfaces.Input;

public interface IGameSessionService
{
    //User Related
    public void AddNewSessionUser(string email);
    public void RemoveSessionUser(string email);
    public void SetSessionForSessionUser(string email, string? sessionId);
    public void JoinSession(string email, string sessionId);
    public void LeaveSession(string email, string sessionId);
    public bool IsUserInSession(string email, string sessionId);
    public List<GameSessionUser> GetAllUsersInSession(string sessionId);
    
    //Session Related
    public void CreateSession(string sessionId);
    public void CreateSession(string sessionId, string sessionName);
    public void PauseSession(string sessionId);
    public GameSession GetSession(string sessionId);
    public List<GameSession> GetAllSessions();
    public List<GameSession> GetAllSessions(string statusLiteral);
}