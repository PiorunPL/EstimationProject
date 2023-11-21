using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.GameSessionContracts;

public record CreateSessionRequest
{
    [Required]
    public string SessionId { get; init; } = null!;

    [Required]
    public string SessionName { get; init; } = null!;
}