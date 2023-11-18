using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.GameSessionContracts;

public record PauseSessionRequest(
    [property: Required] string SessionId);