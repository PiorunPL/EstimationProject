using Domain;
using Services.Interfaces.Input;
using Services.Interfaces.Repository;

namespace Services.Services;

public class GameSessionService : IGameSessionService
{
    private IRepositoryUser _repositoryUser;

    public GameSessionService(IRepositoryUser repositoryUser)
    {
        _repositoryUser = repositoryUser;
    }

    public void JoinSession(string email, string sessionId)
    {
        //TODO: Valid User

        GameSession foundSession = GetGameSession(sessionId);
        User foundUser = GetUser(email);
        
        if (foundSession.IsUserInSession(foundUser))
            throw new ArgumentException($"User with given email ({email}) is already in session ({sessionId})!");

        //TODO: Add Timer to wait for connection from user
        foundSession.AddUser(foundUser);
    }

    public void LeaveSession(string email, string sessionId)
    {
        GameSession foundSession = GetGameSession(sessionId);
        User foundUser = GetUser(email);
        
        if (!foundSession.IsUserInSession(foundUser))
            throw new ArgumentException($"User with given email ({email}) is not in session ({sessionId})!");
        
        foundSession.RemoveUser(foundUser);
    }

    public bool IsUserInSession(string email, string sessionId)
    {
        GameSession foundSession = GetGameSession(sessionId);
        User foundUser = GetUser(email);

        return foundSession.IsUserInSession(foundUser);
    }

    public List<GameSessionUser> GetAllUsersInSession(string sessionId)
    {
        //TODO: Change type from GameSessionUser (where is password stored) to specific type
        GameSession foundSession = GetGameSession(sessionId);
        return foundSession.ActiveUsers.ToList();
    }

    public void CreateSession(string sessionId)
    {
        GameSession.CreateSession(sessionId, sessionId);
    }

    public void CreateSession(string sessionId, string sessionName)
    {
        GameSession.CreateSession(sessionId, sessionName);
    }

    public void CloseSession(string sessionId)
    {
        //TODO: Add validation for user, is he in the session, or is he available to do administrative things
        GameSession foundSession = GetGameSession(sessionId);
        foundSession.CloseSession();
    }

    public GameSession GetSession(string sessionId)
    {
        return GetGameSession(sessionId);
    }

    public List<GameSession> GetAllActiveSessions()
    {
        return GameSession.ActiveGameSessions.ToList();
    }

    public List<GameSession> GetAllClosedSessions()
    {
        return GameSession.ClosedGameSessions.ToList();
    }

    private GameSession GetGameSession(string sessionId)
    {
        GameSession? foundSession = GameSession.ActiveGameSessions.FirstOrDefault(session => session.GameSessionId.Equals(sessionId));
        if (foundSession == null)
            throw new ArgumentException($"Session with given ID ({sessionId}) does not exist!");
        return foundSession;
    }

    private User GetUser(string email)
    {
        User? foundUser = _repositoryUser.GetUser(email);
        if (foundUser == null)
            throw new ArgumentException($"User with given email ({email}) does not exist!");
        return foundUser;
    }
}