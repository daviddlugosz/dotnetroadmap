using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetPublicData")]
        public string GetPublicData()
        {
            return "some public data";
        }

        [HttpGet]
        [Route("GetRestrictedData")]
        public string GetPrivateData()
        {
            return "PRIVATE DATA";
        }
    }
}