using Ardalis.Result;
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
    public Result JoinSession(TokenData tokenData, JoinSessionRequest request);
    public Result LeaveSession(TokenData tokenData, LeaveSessionRequest request);
    public void LeaveSession(string email, string sessionId); //For Hub
    public bool IsUserInSession(string email, string sessionId);
    public Result<List<GameSessionUser>> GetAllUsersInSession(GetUsersInSessionRequest request);
    
    //Session Related
    public Result CreateSession(CreateSessionRequest request);
    public Result PauseSession(PauseSessionRequest request);
    public Result<GameSession> GetSession(GetSessionRequest request);
    public List<GameSession> GetAllSessions();
    public Result<List<GameSession>> GetAllSessions(GetAllSessionsRequest request);
}