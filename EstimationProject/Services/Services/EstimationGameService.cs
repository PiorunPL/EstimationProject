using Domain;

namespace Services.Services;

public class EstimationGameService
{
    // //TODO: Rebuild using Repository pattern
    // public void createNewGameSession(string gameSessionId)
    // {
    //     List<GameSession> activeGameSessions = GameSession.ActiveGameSessions;
    //     if (activeGameSessions.FirstOrDefault(game => game.GameSessionId == gameSessionId) != null)
    //         throw new ArgumentException("GameSession already exists");
    //
    //     GameSession session = new GameSession(gameSessionId);
    //     activeGameSessions.Add(session);
    // }
}