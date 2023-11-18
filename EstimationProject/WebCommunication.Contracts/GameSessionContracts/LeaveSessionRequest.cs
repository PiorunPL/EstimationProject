using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.GameSessionContracts;

public record LeaveSessionRequest
{
    public LeaveSessionRequest(string sessionId)
    {
        SessionId = sessionId;
    }

    [Required]
    public string SessionId { get; init; }
}