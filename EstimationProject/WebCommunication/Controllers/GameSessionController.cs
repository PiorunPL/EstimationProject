using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Input;
using WebApplication1.Other;
using WebCommunication.Contracts.GameSessionContracts;

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
    public IActionResult JoinSession(JoinSessionRequest request)
    {
        try
        {
            _gameSessionService.JoinSession(Helper.GetTokenData(HttpContext), request);
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok();
    }

    [HttpPut("leave")]
    public IActionResult LeaveSession(LeaveSessionRequest request)
    {
        try
        {
            _gameSessionService.LeaveSession(Helper.GetTokenData(HttpContext), request);
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok();
    }

    [HttpGet("users")]
    public IActionResult GetUsersInSession([FromQuery] GetUsersInSessionRequest request)
    {
        List<GameSessionUser> result;
        try
        {
            result = _gameSessionService.GetAllUsersInSession(request);
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok(result);
    }

    [HttpPost("create")]
    public IActionResult CreateSession(CreateSessionRequest request)
    {
        try
        {
            _gameSessionService.CreateSession(request);
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok();
    }

    [HttpPut("pause")]
    public IActionResult PauseSession(PauseSessionRequest request)
    {
        try
        {
            _gameSessionService.PauseSession(request);
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok();
    }

    [HttpGet("sessions")]
    public IActionResult GetAllSessions([FromQuery] GetAllSessionsRequest request)
    {
        List<GameSession> result;
        try
        {
            result = _gameSessionService.GetAllSessions(request);
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok(result);
    }

    [HttpGet("")]
    public IActionResult GetSession([FromQuery] GetSessionRequest request)
    {
        GameSession result;
        try
        {
            result = _gameSessionService.GetSession(request);
        }
        catch (Exception e)
        {
            return BadRequest($"Something went wrong!\nError message: {e.Message}");
        }

        return Ok(result);
    }
    
}