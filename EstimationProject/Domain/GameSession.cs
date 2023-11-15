namespace Domain;

public class GameSession
{
    //TODO: Add list for users which can do important changes for session aka sessionAdministrators
    public string GameSessionId { get; }
    public string GameSessionName { get; set; }
    public GameSessionStatus Status { get; set; }
    public List<GameSessionUser> ActiveUsers { get; } = new List<GameSessionUser>();
    public EstimationGame EstimationGame { get;  } = new EstimationGame();
    
    private GameSession(string id, string name)    {
        GameSessionId = id;
        GameSessionName = name;
    }

    public static GameSession CreateSession(string sessionId, string sessionName, GameSessionStatus status = GameSessionStatus.Inactive)
    {
        GameSession session = new GameSession(sessionId, sessionName)
        {
            Status = status
        };
        return session;
    }

    public void AddUser(GameSessionUser user)
    {
        //TODO: Add next level validation for multiple same users
        ActiveUsers.Add(user);
    }

    public void RemoveUser(string email) {
        GameSessionUser? foundSessionUser = ActiveUsers.FirstOrDefault(sessionUser => sessionUser.User.Email == email);
        if (foundSessionUser != null)
            ActiveUsers.Remove(foundSessionUser);
    }

    public void RemoveUser(GameSessionUser user)
    {
        GameSessionUser? foundSessionUser = ActiveUsers.FirstOrDefault(sessionUser => sessionUser.Equals(user));
        if (foundSessionUser != null)
            ActiveUsers.Remove(foundSessionUser);
    }

    public bool IsUserInSession(string email)
    {
        GameSessionUser? foundSessionUser = ActiveUsers.FirstOrDefault(sessionUser => sessionUser.User.Email == email);
        if (foundSessionUser == null)
            return false;
        return true;
    }

    public bool IsUserInSession(GameSessionUser user)
    {
        return ActiveUsers.Contains(user);
    }
}