using Domain;

namespace Services.Interfaces.Repository;

public interface IRepositoryGameSession
{
    public List<GameSession> GetSessions();
    public List<GameSession> GetSessions(GameSessionStatus status);
    public GameSession? GetSession(string sessionId);
    public void AddSession(GameSession session);
    public void RemoveSession(GameSession session);
    public void RemoveSession(string sessionId);
    public void EditSession(GameSession session, string sessionId);
}