using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.GameSessionContracts;

public record JoinSessionRequest
{
    [Required]
    public string SessionId { get; init; } = null!;
}