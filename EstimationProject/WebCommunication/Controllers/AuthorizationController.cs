using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces.Input;
using WebApplication1.Other;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/authorize")]
public class AuthorizationController : ControllerBase
{
    // private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public AuthorizationController(IConfiguration config, IUserService userService)
    {
        // _config = config;
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
            var result = Helper.GetTokenData(HttpContext);
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
            var result = Helper.GetTokenData(HttpContext);
            return Ok(_userService.RefreshToken(result.email, result.name));
        }
        catch (Exception e)
        {
            return BadRequest("Invalid JWT token!");
        }
    }

}