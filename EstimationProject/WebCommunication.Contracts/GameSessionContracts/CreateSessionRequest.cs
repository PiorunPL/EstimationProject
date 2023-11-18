using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.GameSessionContracts;

public record CreateSessionRequest
{
    [Required]
    public string SessionId { get; init; }
    [Required]
    public string SessionName { get; init; }
    
    public CreateSessionRequest(string sessionId, string sessionName)
    {
        SessionId = sessionId;
        SessionName = sessionName;
    }
}