using Domain;

namespace Services;

public class EstimationGameService
{
    private EstimationGame _estimationGame = new EstimationGame();

    public EstimationGame GetEstimationGame()
    {
        return _estimationGame;
    }

    public void CreateHub()
    {
        _estimationGame.createHub();
    }
}