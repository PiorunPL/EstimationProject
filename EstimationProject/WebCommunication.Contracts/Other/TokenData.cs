namespace WebCommunication.Contracts.Other;

public record TokenData
{
    public TokenData(string email, string username)
    {
        Email = email;
        Username = username;
    }
    
    public string Email { get; init; }
    public string Username { get; init; }
};