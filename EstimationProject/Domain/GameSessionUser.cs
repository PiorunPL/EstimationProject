namespace Domain;

public class GameSessionUser
{
    public static readonly List<GameSessionUser> GameSessionUsers = new List<GameSessionUser>();
    
    public GameSessionUser(User user)
    {
        User = user;
    }

    public User User { get; set; }
    public string? SessionId { get; set; }
    
}