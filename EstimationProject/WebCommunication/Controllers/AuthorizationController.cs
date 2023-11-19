using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Input;
using WebApplication1.Other;
using WebCommunication.Contracts.UserContracts;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;

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
    public ActionResult<string> Register(RegisterUserRequest request)
    {
        var result = _userService.RegisterUser(request);
        return result.ToActionResult(this);
    }
    

    [HttpPost("Login")]
    public ActionResult<string> Login(LoginUserRequest request)
    {
        var result = _userService.LoginUser(request);
        return result.ToActionResult(this);
    }

    [Authorize]
    [HttpDelete("DeleteAccount")]
    public ActionResult DeleteAccount()
    {
        var result = _userService.DeleteUserAccount(Helper.GetTokenData(HttpContext));
        return result.ToActionResult(this);
    }
    
    [Authorize]
    [HttpGet("RefreshToken")]
    public ActionResult<string> RefreshToken()
    {
        var result = _userService.RefreshToken(Helper.GetTokenData(HttpContext));
        return result.ToActionResult(this);
    }

}