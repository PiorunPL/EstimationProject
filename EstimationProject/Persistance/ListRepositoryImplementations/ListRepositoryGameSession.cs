using System.Transactions;
using Domain;
using Services.Interfaces.Repository;

namespace Persistance.ListRepositoryImplementations;

public class ListRepositoryGameSession : IRepositoryGameSession
{
    private readonly List<GameSession> _activeSessions = new List<GameSession>();
    public List<GameSession> GetSessions()
    {
        return _activeSessions.ToList();
    }

    public List<GameSession> GetSessions(GameSessionStatus status)
    {
        return _activeSessions.Where(session => session.Status == status).ToList();
    }

    public GameSession? GetSession(string sessionId)
    {
        return _activeSessions.FirstOrDefault(session => session.GameSessionId.Equals(sessionId));
    }

    public void AddSession(GameSession session)
    {
        lock (_activeSessions)
        {
            var found = GetSession(session.GameSessionId);
            if (found == null)
                _activeSessions.Add(session);
        }
    }

    public void RemoveSession(GameSession session)
    {
        RemoveSession(session.GameSessionId);
    }

    public void RemoveSession(string sessionId)
    {
        lock (_activeSessions)
        {
            var found = GetSession(sessionId);
            if (found != null)
                _activeSessions.Remove(found);
        }
    }

    public void EditSession(GameSession session, string sessionId)
    {
        lock (_activeSessions)
        {
            var found = GetSession(sessionId);
            if (found == null)
                return;
            
            using (TransactionScope scope = new TransactionScope())
            {
                _activeSessions.Remove(found);
                _activeSessions.Add(session);
            }
        }
        // _context.Entry(employee).State = EntityState.Modified; Can be usefull when using database
    }
}