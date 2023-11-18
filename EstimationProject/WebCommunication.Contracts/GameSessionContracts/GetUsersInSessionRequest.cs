using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.GameSessionContracts;

public record GetUsersInSessionRequest
{
    public GetUsersInSessionRequest(string sessionId)
    {
        SessionId = sessionId;
    }

    [Required]
    public string SessionId { get; init; }
}