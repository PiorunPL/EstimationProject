using System.Runtime.InteropServices;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Input;
using WebApplication1.Other;

namespace WebApplication1.Controllers;

[ApiController]
[Authorize]
[Route("api/game-session")]
public class GameSessionController : ControllerBase
{
    private readonly IGameSessionService _gameSessionService;

    public GameSessionController(IGameSessionService gameSessionService)
    {
        _gameSessionService = gameSessionService;
    }

    [HttpPut("join")]
    public IActionResult JoinSession(string sessionId)
    {
        try
        {
            var tokenData = Helper.GetTokenData(HttpContext);
            _gameSessionService.JoinSession(tokenData.email, sessionId);
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok();
    }

    [HttpPut("leave")]
    public IActionResult LeaveSession(string sessionId)
    {
        try
        {
            var tokenData = Helper.GetTokenData(HttpContext);
            _gameSessionService.LeaveSession(tokenData.email, sessionId);
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok();
    }

    [HttpGet("users")]
    public IActionResult GetUsersInSession(string sessionId)
    {
        List<GameSessionUser> result;
        try
        {
            result = _gameSessionService.GetAllUsersInSession(sessionId);
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok(result);
    }

    [HttpPost("create")]
    public IActionResult CreateSession(string sessionId,[Optional] string? sessionName)
    {
        try
        {
            if(sessionName == null)
                _gameSessionService.CreateSession(sessionId);
            else
                _gameSessionService.CreateSession(sessionId, sessionName);
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok();
    }

    [HttpPut("close")]
    public IActionResult CloseSession(string sessionId)
    {
        try
        {
            _gameSessionService.CloseSession(sessionId);
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok();
    }

    [HttpGet("active-sessions")]
    public IActionResult GetAllActiveSessions()
    {
        List<GameSession> result;
        try
        {
            result = _gameSessionService.GetAllActiveSessions();
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok(result);
    }

    [HttpGet("closed-sessions")]
    public IActionResult GetAllClosedSessions()
    {
        List<GameSession> result;
        try
        {
            result = _gameSessionService.GetAllClosedSessions();
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok(result);
    }

    [HttpGet("")]
    public IActionResult GetSession(string sessionId)
    {
        GameSession result;
        try
        {
            result = _gameSessionService.GetSession(sessionId);
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok(result);
    }
    
}