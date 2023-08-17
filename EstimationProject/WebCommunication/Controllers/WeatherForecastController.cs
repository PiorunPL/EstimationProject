using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class  WeatherForecastController : ControllerBase
{
    private EstimationGameService _gameService = new EstimationGameService();
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        // Console.WriteLine("Before creating hub");
        // _gameService.CreateHub();
        // Console.WriteLine("After creating hub");
        //
        // Console.WriteLine("Before getting game");
        // var game = _gameService.GetEstimationGame();
        // Console.WriteLine("After getting game");
        //
        
        // var connectionId = game.GameHub?.Context.ConnectionId;
        // if (connectionId != null)
        // {
        //     Console.WriteLine(connectionId);
        // }
        // else
        // {
        //     Console.WriteLine("connection id is null");
        // }
        //
        // var userIdentifier = game.GameHub?.Context.UserIdentifier;
        // if (userIdentifier != null)
        // {
        //     Console.WriteLine(userIdentifier);
        // }
        // else
        // {
        //     Console.WriteLine("User identifier is null");
        // }
        //
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}