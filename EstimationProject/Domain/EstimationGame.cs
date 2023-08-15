namespace Domain;

public class EstimationGame
{
    public List<Requirement> ToEstimate { get; } = new List<Requirement>();
    public List<Requirement> Estimated { get; } = new List<Requirement>();

    public EstimationGameHub? GameHub = null;

    public void createHub()
    {
        if (GameHub == null)
        {
            GameHub = new EstimationGameHub();
        }
    }
}