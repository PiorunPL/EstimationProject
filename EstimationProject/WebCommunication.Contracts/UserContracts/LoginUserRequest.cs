using System.ComponentModel.DataAnnotations;

namespace WebCommunication.Contracts.UserContracts;

public record LoginUserRequest
{
    public LoginUserRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }

    [Required]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$")]
    public string Email { get; init; }
    
    [Required]
    [MinLength(10)]
    [MaxLength(50)]
    public string Password { get; init; }
}