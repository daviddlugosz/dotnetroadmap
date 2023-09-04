using Microsoft.AspNetCore.Mvc;
using REST.Core.Contracts;
using REST.Core.Repositories;

namespace REST.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IWeatherForecastRepository weatherForecastRepository, 
        ILogger<WeatherForecastController> logger)
    {
        _weatherForecastRepository = weatherForecastRepository;
        _logger = logger;
    }

    [HttpGet("/GetWeatherForecasts")]
    public async Task<IEnumerable<WeatherForecastResponse>> GetAll()
    {
        return await _weatherForecastRepository.GetAll();
    }
}
