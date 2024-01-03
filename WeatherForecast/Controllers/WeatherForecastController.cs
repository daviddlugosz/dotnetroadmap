using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Identity;

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

        [AllowAnonymous]
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

        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpGet]
        [Route("GetAdminData")]
        public string GetAdminData()
        {
            return "!!! ADMIN DATA !!!";
        }
    }
}