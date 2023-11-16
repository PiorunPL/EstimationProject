using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces.Input;
using Services.Interfaces.Repository;
using WebCommunication.Contracts.UserContracts;

namespace Services.Services;

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
        //TODO: INPUT VALIDATION
        
        var foundUser = _repositoryUser.GetUser(request.Email);
        if (foundUser != null)
            throw new ArgumentException();
        
        //TODO: HASHING PASSWORD

        User user = new User(request.Email,request.Username, request.Password);
        
        _repositoryUser.AddUser(user);

        return GenerateToken(user);
    }

    public string LoginUser(string email, string password)
    {
        var foundUser = _repositoryUser.GetUser(email);
        if (foundUser == null)
            throw new ArgumentException();

        if (foundUser.Password != password)
            throw new ArgumentException();

        return GenerateToken(foundUser);
    }

    public void RemoveUser(string email)
    {
        var foundUser = _repositoryUser.GetUser(email);
        if (foundUser == null)
            throw new ArgumentException();

        _repositoryUser.RemoveUser(foundUser);
    }

    public string RefreshToken(string email, string name)
    {
        return GenerateToken(email, name);
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