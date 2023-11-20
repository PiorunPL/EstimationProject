using System.Transactions;
using Ardalis.GuardClauses;
using Ardalis.Result;
using Domain;
using Services.Exceptions;
using Services.Interfaces.Input;
using Services.Interfaces.Repository;
using WebCommunication.Contracts.GameSessionContracts;
using WebCommunication.Contracts.Other;

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

        var foundUser = GetUser(email);

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

    public Result JoinSession(TokenData tokenData, JoinSessionRequest request)
    {
        //TODO: Valid User

        GameSession foundSession = GetGameSession(request.SessionId);
        if (foundSession.IsUserInSession(tokenData.Email))
            return Result.Conflict($"User with given email ({tokenData.Email}) is already in session ({request.SessionId})!");

        var foundSessionUser = _repositoryGameSessionUser.GetUser(tokenData.Email);
        if(foundSessionUser == null)
            return Result.NotFound($"SessionUser (with email: {tokenData.Email}) is not added yet!");
        
        //TODO: Add Timer to wait for connection from user

        using (TransactionScope scope = new TransactionScope())
        {
            foundSession.AddUser(foundSessionUser);
            SetSessionForSessionUser(tokenData.Email, request.SessionId);
        }

        return Result.Success();
    }

    public Result LeaveSession(TokenData tokenData, LeaveSessionRequest request)
    {
        try
        {
            LeaveSession(tokenData.Email, request.SessionId);
        }
        catch (UserNotFoundException e)
        {
            return Result.NotFound(e.Message);
        }
        catch (UserNotInSessionException e)
        {
            return Result.Conflict(e.Message);
        }
        
        return Result.Success();
    }

    public void LeaveSession(string email, string sessionId)
    {
        GameSession foundSession = GetGameSession(sessionId);
        var foundSessionUser = _repositoryGameSessionUser.GetUser(email);
        if(foundSessionUser == null)
            throw new UserNotFoundException($"User with given email ({email}) does not exist!");
        
        if (!foundSession.IsUserInSession(email))
            throw new UserNotInSessionException($"User with given email ({email}) is not in session ({sessionId})!");
        
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

    public Result<List<GameSessionUser>> GetAllUsersInSession(GetUsersInSessionRequest request)
    {
        //TODO: Change type from GameSessionUser (where is password stored) to specific type
        try
        {
            return Result.Success(GetGameSession(request.SessionId).ActiveUsers.ToList());
        }
        catch (SessionNotFoundException e)
        {
            return Result.NotFound(e.Message);
        }
    }

    public Result CreateSession(CreateSessionRequest request)
    {
        try
        {
            CreateSessionUniversal(request.SessionId,request.SessionName, GameSessionStatus.Active);
        }
        catch (SessionAlreadyExistException e)
        {
            return Result.Conflict(e.Message);
        }
        return Result.Success();
    }

    private void CreateSessionUniversal(string sessionId, string sessionName, GameSessionStatus status)
    {
        GameSession? foundActiveGameSession = _repositoryGame.GetSession(sessionId);
        if (foundActiveGameSession != null)
            throw new SessionAlreadyExistException($"Session with given ID ({sessionId}) already exists!");

        GameSession session = GameSession.CreateSession(sessionId, sessionName, status);
        _repositoryGame.AddSession(session);
    }
    

    public Result PauseSession(PauseSessionRequest request)
    {
        //TODO: Add validation for user, is he in the session, or is he available to do administrative things
        //TODO: Check for status of session, if it is active, then pause it, if it is paused, then return conflict
        try
        {
            GameSession foundSession = GetGameSession(request.SessionId);
            foundSession.Status = GameSessionStatus.Inactive;
        }
        catch (SessionNotFoundException e)
        {
            return Result.NotFound(e.Message);
        }
        
        return Result.Success();
    }

    public Result<GameSession> GetSession(GetSessionRequest request)
    {
        try
        {
            return Result<GameSession>.Success(GetGameSession(request.SessionId));
        }
        catch (SessionNotFoundException e)
        {
            return Result<GameSession>.NotFound(e.Message);
        }
    }
    public List<GameSession> GetAllSessions()
    {
        return  _repositoryGame.GetSessions();
    }

    public Result<List<GameSession>> GetAllSessions(GetAllSessionsRequest request)
    {
        if(request.Status == null)
            return Result.Success(GetAllSessions());
        
        bool result = Enum.TryParse(request.Status, out GameSessionStatus status); 
        if (!result)
        {
            return Result.Conflict($"Given status ({request.Status}) does not exist!");
        }

        return Result.Success(_repositoryGame.GetSessions(status));
    }

    private GameSession GetGameSession(string sessionId)
    {
        GameSession? foundSession = _repositoryGame.GetSession(sessionId);
        if (foundSession == null)
            throw new SessionNotFoundException($"Session with given ID ({sessionId}) does not exist!");
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