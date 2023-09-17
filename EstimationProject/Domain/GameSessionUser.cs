namespace Domain;

public class GameSessionUser
{
    public GameSessionUser(User user)
    {
        User = user;
    }

    public User User { get; set; }
    public string ConnectionString { get; set; }
    
}