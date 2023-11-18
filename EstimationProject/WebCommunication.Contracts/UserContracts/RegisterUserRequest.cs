using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.UserContracts;

public record RegisterUserRequest
{
    [Required]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$")]
    public string Email { get; init; }
    
    [Required]
    [MinLength(5)]
    [MaxLength(100)]
    public string Username{ get; init; }
    
    [Required]
    [MinLength(10)]
    [MaxLength(50)]
    public string Password{ get; init; }

    public RegisterUserRequest(string email, string username, string password)
    {
        Email = email;
        Username = username;
        Password = password;
    }
};