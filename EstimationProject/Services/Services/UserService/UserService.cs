using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces.Input;
using Services.Interfaces.Repository;
using Services.Services.UserService.Validators;
using WebCommunication.Contracts.Other;
using WebCommunication.Contracts.UserContracts;

namespace Services.Services.UserService;

public class UserService : IUserService
{
    private readonly IRepositoryUser _repositoryUser;
    private readonly IConfiguration _config;

    public UserService(IConfiguration config, IRepositoryUser repositoryUser)
    {
        _repositoryUser = repositoryUser;
        _config = config;
    }


    public Result<string> RegisterUser(RegisterUserRequest request)
    {
        var validationResult = new RegisterUserRequestValidator().Validate(request);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        
        var foundUser = _repositoryUser.GetUser(request.Email);
        if (foundUser != null)
            return Result.Conflict("User with given email already exists");

        var hashedPassword = Password.CreateHashedPassword(request.Password);

        User user = new User(request.Email,request.Username, hashedPassword);
        _repositoryUser.AddUser(user);

        return Result.Success(GenerateToken(user));
    }

    public Result<string> LoginUser(LoginUserRequest request)
    {
        //TODO: Create Validators and return Result.Invalid if invalid
        // new LoginUserRequestValidator().ValidateAndThrow(request); 
        
        var foundUser = _repositoryUser.GetUser(request.Email);
        if (foundUser == null)
            return Result.NotFound("User with given email does not exist");

        if (!foundUser.Password.ComparePassword(request.Password))
            return Result.Unauthorized(); //TODO: Potentially to change return HTTP code

        return Result.Success(GenerateToken(foundUser));
    }

    public Result DeleteUserAccount(TokenData tokenData)
    {
        var foundUser = _repositoryUser.GetUser(tokenData.Email);
        if (foundUser == null)
            return Result.NotFound("User with given email does not exist");

        _repositoryUser.RemoveUser(foundUser);
        return Result.Success();
    }

    public Result<string> RefreshToken(TokenData tokenData)
    {
        //TODO: Add validation if user still exist
        return Result.Success(GenerateToken(tokenData.Email, tokenData.Username));
    }

    public string GenerateToken(User user)
    {
        return GenerateToken(user.Email, user.Username);
    }
    
    public string GenerateToken( string email, string name)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Email, email)
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}