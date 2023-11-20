namespace Services.Exceptions;

public class SessionAlreadyExistException : Exception
{
    public SessionAlreadyExistException(string message) : base(message)
    {
    }
}