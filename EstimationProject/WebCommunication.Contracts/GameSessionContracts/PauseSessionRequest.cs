using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.GameSessionContracts;

public record PauseSessionRequest
{
    [Required]
    public string SessionId { get; init; } = null!;
};