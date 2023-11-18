using Domain;
using WebCommunication.Contracts.GameSessionContracts;
using WebCommunication.Contracts.Other;

namespace Services.Interfaces.Input;

public interface IGameSessionService
{
    //User Related
    public void AddNewSessionUser(string email); 
    public void RemoveSessionUser(string email);
    public void SetSessionForSessionUser(string email, string? sessionId);
    public void JoinSession(TokenData tokenData, JoinSessionRequest request);
    public void LeaveSession(TokenData tokenData, LeaveSessionRequest request);
    public void LeaveSession(string email, string sessionId); //For Hub
    public bool IsUserInSession(string email, string sessionId);
    public List<GameSessionUser> GetAllUsersInSession(GetUsersInSessionRequest request);
    
    //Session Related
    public void CreateSession(CreateSessionRequest request);
    public void PauseSession(PauseSessionRequest request);
    public GameSession GetSession(GetSessionRequest request);
    public List<GameSession> GetAllSessions();
    public List<GameSession> GetAllSessions(GetAllSessionsRequest request);
}