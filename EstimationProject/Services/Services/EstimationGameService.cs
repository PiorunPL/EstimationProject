using Domain;

namespace Services.Services;

public class EstimationGameService
{
    private EstimationGame _estimationGame = new EstimationGame();

    public EstimationGame GetEstimationGame()
    {
        return _estimationGame;
    }
}