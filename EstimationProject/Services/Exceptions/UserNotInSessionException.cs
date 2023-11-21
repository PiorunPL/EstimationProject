namespace Services.Exceptions;

public class UserNotInSessionException : Exception
{
    public UserNotInSessionException(string message) : base(message)
    {
    }
}