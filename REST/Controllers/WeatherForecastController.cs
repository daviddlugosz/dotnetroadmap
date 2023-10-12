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

    [HttpGet("/GetWeatherForecasts/{days:min(1):max(10):int?}")]
    public async Task<IEnumerable<WeatherForecastResponse>> GetAll(int? minTemp, int? maxTemp, int days = 5)
    {
        return await _weatherForecastRepository.GetAll(days, minTemp, maxTemp);
    }

    [HttpGet("/summary/{date:datetime:regex(^\\d{{4}}-\\d{{2}}-\\d{{2}})}")]
    public async Task<IEnumerable<WeatherForecastResponse>> GetForDate(DateTime date)
    {
        return await _weatherForecastRepository.GetForDate(date);
    }

    [HttpGet("/data/{*filePath}")]
    public async Task<string> GetData(string filePath)
    {
        return filePath;    
    }
}
