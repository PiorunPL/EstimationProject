using WebCommunication.Contracts.Other;
using WebCommunication.Contracts.UserContracts;

namespace Services.Interfaces.Input;

public interface IUserService
{
    public string RegisterUser(RegisterUserRequest request);
    public string LoginUser(LoginUserRequest request);
    public void DeleteUserAccount(TokenData tokenData);
    public string RefreshToken(TokenData tokenData);

}