namespace Domain;

public class GameSession
{
    //TODO: Add list for users which can do important changes for session aka sessionAdministrators
    public static readonly List<GameSession> ActiveGameSessions = new List<GameSession>();
    public static readonly List<GameSession> ClosedGameSessions = new List<GameSession>(); //What to do with closed game sessions?

    public string GameSessionId { get; }
    public string GameSessionName { get; set; }
    public List<GameSessionUser> ActiveUsers { get; } = new List<GameSessionUser>();
    public EstimationGame EstimationGame { get;  }= new EstimationGame();
    
    private GameSession(string id, string name)
    {
        GameSessionId = id;
        GameSessionName = name;
        
    }

    public static void CreateSession(string sessionId, string sessionName)
    {
        GameSession? foundActiveGameSession =
            ActiveGameSessions.FirstOrDefault(session => session.GameSessionId.Equals(sessionId));
        if (foundActiveGameSession != null)
            throw new ArgumentException($"Session with given ID ({sessionId}) already exists!");
        
        //TODO: Add validation for ClosedGameSessions
        
        GameSession session = new GameSession(sessionId, sessionName);
        ActiveGameSessions.Add(session);
    }

    public void CloseSession()
    {
        if (ActiveGameSessions.Contains(this))
            ActiveGameSessions.Remove(this);
        
        if (!ClosedGameSessions.Contains(this))
            ClosedGameSessions.Add(this);
    }

    public void AddUser(User user)
    {
        //TODO: Add next level validation for multiple same users;
        GameSessionUser sessionUser = new GameSessionUser(user);
        ActiveUsers.Add(sessionUser);
    }

    public void RemoveUser(User user)
    {
        GameSessionUser? foundSessionUser = ActiveUsers.FirstOrDefault(sessionUser => sessionUser.User.Equals(user));
        if (foundSessionUser != null)
            ActiveUsers.Remove(foundSessionUser);
    }

    public bool IsUserInSession(User user)
    {
        GameSessionUser? foundSessionUser = ActiveUsers.FirstOrDefault(sessionUser => sessionUser.User.Equals(user));
        if (foundSessionUser == null)
            return false;
        return true;
    }
}