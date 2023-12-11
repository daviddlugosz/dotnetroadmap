using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Controllers
{
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

        [HttpGet("{days:range(1,10)}")]
        public IEnumerable<WeatherForecast> Get(int minTemp, int maxTemp, int? days = 0)
        {
            var weatherData = GetWeatherData(days);

            return weatherData
                .Where(forecast => forecast.TemperatureC >= minTemp && forecast.TemperatureC <= maxTemp)
                .ToList();
        }

        private IEnumerable<WeatherForecast> GetWeatherData(int? days)
        {
            var numberOfDays = (days == null || days == 0) ? 5 : days;

            return Enumerable.Range(1, (int)numberOfDays).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}