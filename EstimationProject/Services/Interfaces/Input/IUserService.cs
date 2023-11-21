using Ardalis.Result;
using WebCommunication.Contracts.Other;
using WebCommunication.Contracts.UserContracts;

namespace Services.Interfaces.Input;

public interface IUserService
{
    public Result<string> RegisterUser(RegisterUserRequest request);
    public Result<string> LoginUser(LoginUserRequest request);
    public Result DeleteUserAccount(TokenData tokenData);
    public Result<string> RefreshToken(TokenData tokenData);

}