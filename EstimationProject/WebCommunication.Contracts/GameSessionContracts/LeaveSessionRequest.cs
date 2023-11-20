using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.GameSessionContracts;

public record LeaveSessionRequest
{
    [Required]
    public string SessionId { get; init; } = null!;
}