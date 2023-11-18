using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.GameSessionContracts;

public record GetSessionRequest
{
    public GetSessionRequest(string sessionId)
    {
        SessionId = sessionId;
    }

    [Required]
    public string SessionId { get; init; }
}