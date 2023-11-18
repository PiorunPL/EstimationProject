using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.GameSessionContracts;

public record JoinSessionRequest
{
    public JoinSessionRequest(string sessionId)
    {
        SessionId = sessionId;
    }

    [Required]
    public string SessionId { get; init; }
}