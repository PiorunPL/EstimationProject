namespace WebCommunication.Contracts.GameSessionContracts;

public record GetAllSessionsRequest
{ 
    public string? Status { get; init; }
}