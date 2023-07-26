using Microsoft.AspNetCore.Mvc;
using REST.FilterAttributes;

namespace REST.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
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
        return GenerateWeatherForecasts().ToArray();
    }

    [HttpGet("/Error", Name = "InvokeError")]
    public void Error()
    {
        throw new Exception("This is default handled exception message");
    }

    [HttpGet("/Forecasts", Name = "Forecasts")]
    [DoubleParameterActionFilter]
    public IEnumerable<WeatherForecast> Forecasts([FromQuery] int numberOfForecasts)
    {
        return GenerateWeatherForecasts(numberOfForecasts).ToArray();
    }

    private IEnumerable<WeatherForecast> GenerateWeatherForecasts(int numberOfForecasts = 5)
    {
        return Enumerable.Range(1, numberOfForecasts).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });
    }
}