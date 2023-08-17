namespace Domain;

public class GameSession
{
    public static List<GameSession> ActiveGameSessions = new List<GameSession>();

    public List<GameSessionUser> ActiveUsers = new List<GameSessionUser>();
    
}