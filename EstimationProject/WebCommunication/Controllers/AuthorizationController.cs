using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Input;
using WebApplication1.Other;
using WebCommunication.Contracts.UserContracts;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/authorize")]
public class AuthorizationController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthorizationController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("Register")]
    public IActionResult Register(RegisterUserRequest request)
    {
        string generatedToken;
        try
        {
            generatedToken =  _userService.RegisterUser(request);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(generatedToken);
    }
    

    [HttpGet("Login")]
    public IActionResult Login([FromQuery] LoginUserRequest request)
    {
        string generatedToken;
        try
        {
            generatedToken = _userService.LoginUser(request);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(generatedToken);
    }

    [Authorize]
    [HttpDelete("DeleteAccount")]
    public IActionResult DeleteAccount()
    {
        try
        {
            _userService.DeleteUserAccount(Helper.GetTokenData(HttpContext));
            
        }
        catch (Exception e)
        {
            return BadRequest("Invalid JWT token!"); //TODO: Change exception message 
        }
        return Ok();
    }
    
    [Authorize]
    [HttpGet("RefreshToken")]
    public IActionResult RefreshToken()
    {
        try
        {
            return Ok(_userService.RefreshToken(Helper.GetTokenData(HttpContext)));
        }
        catch (Exception e)
        {
            return BadRequest("Invalid JWT token!"); //TODO: Change exception message
        }
    }

}