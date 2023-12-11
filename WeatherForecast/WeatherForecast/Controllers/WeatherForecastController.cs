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
        public ActionResult<IEnumerable<WeatherForecast>> Get(int minTemp, int maxTemp, int? days = 0)
        {
            var weatherData = GetWeatherData(days);

            return Ok(weatherData
                .Where(forecast => forecast.TemperatureC >= minTemp && forecast.TemperatureC <= maxTemp)
                .ToList()); // 200 OK
        }

        [HttpGet]
        [Route("/summary/{date:regex(^\\d{{4}}\\-(0[[1-9]]|1[[012]])\\-(0[[1-9]]|[[12]][[0-9]]|3[[01]])$)}")]    // constraint to a route template application (using ReGex expression). Query with string parameter not matching 'yyyy-MM-dd' date template will not hit this endpoint at all.
        public ActionResult<WeatherForecast> Get(string date)
        {
            var weatherData = GetWeatherData(31);

            var result = weatherData.FirstOrDefault(day => day.Date == DateOnly.ParseExact(date, "yyyy-MM-dd"));

            if (result == null)
            {
                return NotFound(); // 404 Not Found
            }

            return Ok(result); // 200 OK
        }

        [HttpGet]
        [Route("/data/{filepath:regex(^([[a-zA-Z]]\\:|\\\\\\\\[[^\\/\\\\:*?\"<>|]]+\\\\[[^\\/\\\\:*?\"<>|]]+)(\\\\[[^\\/\\\\:*?\"<>|]]+)+(\\.[[^\\/\\\\:*?\"<>|]]+)$)}")]
        public ActionResult<string> GetData(string filepath)
        {
            var filename = Path.GetFileName(filepath);

            if (string.IsNullOrEmpty(filename))
            {
                return BadRequest(); // 400 Bad Request
            }

            return Ok(filename); // 200 OK
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