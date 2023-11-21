using Ardalis.Result.AspNetCore;
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
    public ActionResult JoinSession(JoinSessionRequest request)
    {
        var result = _gameSessionService.JoinSession(Helper.GetTokenData(HttpContext), request);
        return result.ToActionResult(this);
    }

    [HttpPut("leave")]
    public ActionResult LeaveSession(LeaveSessionRequest request)
    {
        var result = _gameSessionService.LeaveSession(Helper.GetTokenData(HttpContext), request);
        return result.ToActionResult(this);
    }

    [HttpGet("users")]
    public ActionResult<List<GameSessionUser>> GetUsersInSession([FromQuery] GetUsersInSessionRequest request)
    {
        var result = _gameSessionService.GetAllUsersInSession(request);
        return result.ToActionResult(this);
    }

    [HttpPost("create")]
    public ActionResult CreateSession(CreateSessionRequest request)
    {
        var result = _gameSessionService.CreateSession(request);
        return result.ToActionResult(this);
    }

    [HttpPut("pause")]
    public ActionResult PauseSession(PauseSessionRequest request)
    {
        var result = _gameSessionService.PauseSession(request);
        return result.ToActionResult(this);
    }

    [HttpGet("sessions")]
    public ActionResult<List<GameSession>> GetAllSessions([FromQuery] GetAllSessionsRequest request)
    {
        var result = _gameSessionService.GetAllSessions(request);  
        return result.ToActionResult(this);
    }

    [HttpGet("")]
    public ActionResult<GameSession> GetSession([FromQuery] GetSessionRequest request)
    {
        var result = _gameSessionService.GetSession(request);
        return result.ToActionResult(this);
    }
    
}