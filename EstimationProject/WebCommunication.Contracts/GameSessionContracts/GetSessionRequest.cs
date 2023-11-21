using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.GameSessionContracts;

public record GetSessionRequest
{
    [Required]
    public string SessionId { get; init; } = null!;
}