using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces.Input;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/authorize")]
public class AuthorizationController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public AuthorizationController(IConfiguration config, IUserService userService)
    {
        _config = config;
        _userService = userService;
    }
    
    [HttpPost("Register")]
    public IActionResult Register(string email, string username, string password)
    {
        string generatedToken;
        try
        {
            generatedToken =  _userService.RegisterUser(email, username, password);
        }
        catch (Exception e)
        {
            return BadRequest("Invalid input!");
        }

        return Ok(generatedToken);
    }
    

    [HttpGet("Login")]
    public IActionResult Login(string email, string password)
    {
        string generatedToken;
        try
        {
            generatedToken = _userService.LoginUser(email, password);
        }
        catch (Exception e)
        {
            return BadRequest("Invalid input!");
        }

        return Ok(generatedToken);
    }

    [Authorize]
    [HttpDelete("DeleteAccount")]
    public IActionResult DeleteAccount()
    {
        try
        {
            var result = GetTokenData();
            _userService.RemoveUser(result.email);
            
        }
        catch (Exception e)
        {
            return BadRequest("Invalid JWT token!");
        }
        return Ok();
    }
    
    [Authorize]
    [HttpGet("RefreshToken")]
    public IActionResult RefreshToken()
    {
        try
        {
            var result = GetTokenData();
            return Ok(_userService.RefreshToken(result.email, result.name));
        }
        catch (Exception e)
        {
            return BadRequest("Invalid JWT token!");
        }
    }

    private (string email, string name) GetTokenData()
    {
        string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        
        // Console.WriteLine(token);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        string name;
        string email;
        
        
        // Console.WriteLine("Before Token Descriptor");
        var tokenDescriptor = tokenHandler.ReadJwtToken(token);
        var claims = tokenDescriptor.Claims.ToList();
        // Console.WriteLine("After getting claims");

        var nameClaim = claims.First(claim => claim.Type.Contains("name"));
        name = nameClaim.Value;
        // Console.WriteLine("After getting name claim");
            
        // Console.WriteLine(name);

        var emailClaim = claims.First(claim => claim.Type.Contains("emailaddress"));
        email = emailClaim.Value;
        // Console.WriteLine("After getting email claim");
            
        // Console.WriteLine(email);

        return (email, name);
    }
    
    

    
    
}