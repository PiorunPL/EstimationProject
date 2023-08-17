namespace Domain;

public class User
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } //TODO Change plaintext password to hashed form

    public User(string email, string username, string password)
    {
        Email = email;
        Username = username;
        Password = password;
    }
}