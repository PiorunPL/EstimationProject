namespace Domain;

public enum GameSessionStatus
{
    Active, // Currently in use
    Inactive, // Can become Active in any moment
    ViewOnly // Session has ended
}