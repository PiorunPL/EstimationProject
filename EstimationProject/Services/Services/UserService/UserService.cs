using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using FluentValidation;
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


    public string RegisterUser(RegisterUserRequest request)
    {
        new RegisterUserRequestValidator().ValidateAndThrow(request); 
        
        var foundUser = _repositoryUser.GetUser(request.Email);
        if (foundUser != null)
            throw new ArgumentException();

        var hashedPassword = Password.CreateHashedPassword(request.Password);

        User user = new User(request.Email,request.Username, hashedPassword);
        
        _repositoryUser.AddUser(user);

        return GenerateToken(user);
    }

    public string LoginUser(LoginUserRequest request)
    {
        //TODO: Create Validators
        // new LoginUserRequestValidator().ValidateAndThrow(request); 
        
        var foundUser = _repositoryUser.GetUser(request.Email);
        if (foundUser == null)
            throw new ArgumentException("Login data invalid");

        if (foundUser.Password.ComparePassword(request.Password))
            throw new ArgumentException("Login data invalid ");

        return GenerateToken(foundUser);
    }

    public void DeleteUserAccount(TokenData tokenData)
    {
        var foundUser = _repositoryUser.GetUser(tokenData.Email);
        if (foundUser == null)
            throw new ArgumentException();

        _repositoryUser.RemoveUser(foundUser);
    }

    public string RefreshToken(TokenData tokenData)
    {
        return GenerateToken(tokenData.Email, tokenData.Username);
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