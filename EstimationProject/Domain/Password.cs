using System.Security.Cryptography;
using System.Text;

namespace Domain;

public class Password
{
    public const int KeySize = 128; //TODO: Load it from configuration file
    public const int Iterations = 100000; //TODO: Load it from configuration file

    public string HashedPassword { get; set; }
    public string Salt { get; set; }
    
    private Password(string hashedPassword, string salt)
    {
        HashedPassword = hashedPassword;
        Salt = salt;
    }

    public static Password CreateHashedPassword(string plainTextPassword)
    {
        var salt = RandomNumberGenerator.GetBytes(KeySize);
        var hash = HashPassword(plainTextPassword, salt);
        
        var hashText = Encoding.UTF8.GetString(hash);
        var saltText = Encoding.UTF8.GetString(salt);
        
        var password = new Password(hashText, saltText);
        return password;
    }

    public bool ComparePassword(string passwordToCompare)
    {
        var hashToCompare = HashPassword(passwordToCompare, Encoding.UTF8.GetBytes(Salt));
        var hashTextToCompare = Encoding.UTF8.GetString(hashToCompare);

        return HashedPassword.Equals(hashTextToCompare);
    }

    private static byte[] HashPassword(string plainTextPassword, byte[] salt)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(plainTextPassword),
            salt,
            Iterations,
            HashAlgorithmName.SHA512,
            KeySize);
        return hash;
    }
}