namespace Domain;

public class User
{
    public string Email { get; set; }
    public string Username { get; set; }
    public Password Password { get; set; }

    public User(string email, string username, Password password)
    {
        Email = email;
        Username = username;
        Password = password;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not User user)
            return false;

        return Email.Equals(user.Email);
    }

    protected bool Equals(User other)
    {
        return Email == other.Email;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Email, Username, Password);
    }
}