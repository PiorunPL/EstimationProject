using WebCommunication.Contracts.UserContracts;

namespace Services.Interfaces.Input;

public interface IUserService
{
    public string RegisterUser(RegisterUserRequest request);
    public string LoginUser(string email, string password);
    public void RemoveUser(string email);
    public string RefreshToken(string email, string name);

}