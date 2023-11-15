using System.Transactions;
using Domain;
using Services.Interfaces.Input;
using Services.Interfaces.Repository;

namespace Services.Services;

public class GameSessionService : IGameSessionService
{
    private readonly IRepositoryUser _repositoryUser;
    private readonly IRepositoryGameSession _repositoryGame;
    private readonly IRepositoryGameSessionUser _repositoryGameSessionUser;
    public GameSessionService(IRepositoryUser repositoryUser, IRepositoryGameSession repositoryGame, IRepositoryGameSessionUser repositoryGameSessionUser)
    {
        _repositoryUser = repositoryUser;
        _repositoryGame = repositoryGame;
        _repositoryGameSessionUser = repositoryGameSessionUser;
    }

    public void AddNewSessionUser(string email)
    {
        var foundSessionUser = _repositoryGameSessionUser.GetUser(email);
        if (foundSessionUser != null)
            throw new InvalidOperationException($"SessionUser (with email: {email}) is already added!");

        var foundUser = _repositoryUser.GetUser(email);
        if (foundUser == null)
            throw new ArgumentException($"User (with email: {email}) does not exist!");

        var sessionUser = new GameSessionUser(foundUser);
        _repositoryGameSessionUser.AddUser(sessionUser);
    }

    public void RemoveSessionUser(string email)
    {
        var foundSessionUser = _repositoryGameSessionUser.GetUser(email);
        if (foundSessionUser == null)
            throw new ArgumentException($"SessionUser (with email: {email}) is not added yet!");

        _repositoryGameSessionUser.RemoveUser(foundSessionUser);
    }

    public void SetSessionForSessionUser(string email, string? sessionId)
    {
        var foundSessionUser = _repositoryGameSessionUser.GetUser(email);
        if (foundSessionUser == null)
            throw new ArgumentException($"SessionUser (with email: {email}) is not added yet!");
        
        foundSessionUser.SessionId = sessionId;
    }

    public void JoinSession(string email, string sessionId)
    {
        //TODO: Valid User

        GameSession foundSession = GetGameSession(sessionId);
        if (foundSession.IsUserInSession(email))
            throw new ArgumentException($"User with given email ({email}) is already in session ({sessionId})!");

        var foundSessionUser = _repositoryGameSessionUser.GetUser(email);
        if(foundSessionUser == null)
            throw new ArgumentException($"SessionUser (with email: {email}) is not added yet!");
        
        //TODO: Add Timer to wait for connection from user

        using (TransactionScope scope = new TransactionScope())
        {
            foundSession.AddUser(foundSessionUser);
            SetSessionForSessionUser(email, sessionId);
        }
    }

    public void LeaveSession(string email, string sessionId)
    {
        GameSession foundSession = GetGameSession(sessionId);
        var foundSessionUser = _repositoryGameSessionUser.GetUser(email);
        if(foundSessionUser == null)
            throw new ArgumentException($"SessionUser (with email: {email}) is not added yet!");
        
        if (!foundSession.IsUserInSession(email))
            throw new ArgumentException($"User with given email ({email}) is not in session ({sessionId})!");
        
        using (TransactionScope scope = new TransactionScope())
        {
            foundSession.RemoveUser(foundSessionUser);
            SetSessionForSessionUser(email, null);
        }
    }

    public bool IsUserInSession(string email, string sessionId)
    {
        GameSession foundSession = GetGameSession(sessionId);
        return foundSession.IsUserInSession(email);
    }

    public List<GameSessionUser> GetAllUsersInSession(string sessionId)
    {
        //TODO: Change type from GameSessionUser (where is password stored) to specific type
        GameSession foundSession = GetGameSession(sessionId);
        return foundSession.ActiveUsers.ToList();
    }

    public void CreateSession(string sessionId)
    {
        CreateSessionUniversal(sessionId,sessionId, GameSessionStatus.Active);
    }

    public void CreateSession(string sessionId, string sessionName)
    {
        CreateSessionUniversal(sessionId, sessionName, GameSessionStatus.Active);
    }

    private void CreateSessionUniversal(string sessionId, string sessionName, GameSessionStatus status)
    {
        GameSession? foundActiveGameSession = _repositoryGame.GetSession(sessionId);
        if (foundActiveGameSession != null)
            throw new Exception($"Session with given ID ({sessionId}) already exists!");

        GameSession session = GameSession.CreateSession(sessionId, sessionName, status);
        _repositoryGame.AddSession(session);
    }
    

    public void PauseSession(string sessionId)
    {
        //TODO: Add validation for user, is he in the session, or is he available to do administrative things
        GameSession foundSession = GetGameSession(sessionId);
        foundSession.Status = GameSessionStatus.Inactive;
    }

    public GameSession GetSession(string sessionId)
    {
        return GetGameSession(sessionId);
    }
    public List<GameSession> GetAllSessions()
    {
        return  _repositoryGame.GetSessions();
    }

    public List<GameSession> GetAllSessions(string statusLiteral)
    {
        bool result = Enum.TryParse(statusLiteral, out GameSessionStatus status);

        if (result!) {
            throw new ArgumentException($"Given status ({statusLiteral}) does not exist!");
        }

        return _repositoryGame.GetSessions(status);
    }

    private GameSession GetGameSession(string sessionId)
    {
        GameSession? foundSession = _repositoryGame.GetSession(sessionId);
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