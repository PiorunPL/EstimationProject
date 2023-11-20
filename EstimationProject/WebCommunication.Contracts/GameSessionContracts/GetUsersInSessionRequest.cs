using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.GameSessionContracts;

public record GetUsersInSessionRequest
{
    [Required]
    public string SessionId { get; init; } = null!;
}