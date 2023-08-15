using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/authorize")]
public class AuthorizationController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthorizationController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("GetAuthorizationToken")]
    public string Get(string name, string email)
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