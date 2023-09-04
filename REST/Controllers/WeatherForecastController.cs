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
    public async Task<ActionResult<IEnumerable<WeatherForecastResponse>>> GetAll()
    {
        var items = await _weatherForecastRepository.GetAll();
        return Ok(items);
    }

    // Assignment 2
    [HttpGet("/GetWeatherForecastById")]
    public async Task<ActionResult<WeatherForecastResponse>> Get(int id)
    {
        return Ok(await _weatherForecastRepository.Get(id));
    }

    // Assignment 3
    [HttpPost("/CreateWeatherForecast")]
    public async Task<ActionResult<WeatherForecastResponse>> Add(CreateWeatherForecastRequest weatherForecast)
    {
        var createdItem = await _weatherForecastRepository.Create(weatherForecast);
        return Created($"/GetWeatherForecastById/{createdItem.Id}", createdItem));
    }

    // Assignment 4
    [HttpPut("/UpdateWeatherForecast")]
    public async Task<WeatherForecastResponse> Update(UpdateWeatherForecastRequest weatherForecast)
    {
        return await _weatherForecastRepository.Update(weatherForecast);
    }

    // Assignment 5
    [HttpDelete("/DeleteWeatherForecast")]
    public async Task<ActionResult> Delete(int id)
    {
        await _weatherForecastRepository.Delete(id);

        return NoContent();
    }
}
