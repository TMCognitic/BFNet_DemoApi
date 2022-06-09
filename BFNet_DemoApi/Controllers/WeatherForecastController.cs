using Microsoft.AspNetCore.Mvc;

namespace BFNet_DemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //https://localhost:7275/api/WeatherForecast
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

        //https://localhost:7275/api/WeatherForecast/{id}
        [HttpGet("{id}")]
        public IEnumerable<WeatherForecast> Get(int id)
        {
            _logger.LogInformation("WeatherForecast Get called!");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}