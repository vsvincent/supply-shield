using Gateway.Extensions;
using Gateway.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
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
        [Authorize]
        [HttpGet("GetWeatherForecast")]
        public IActionResult Get()
        {
            FirebaseUser user = HttpContext.GetFirebaseUser();

            return Ok(user);
        }
        [HttpGet("test")]
        public IActionResult GetTest()
        {
            return Ok("This should be some cool text.");
        }
    }
}