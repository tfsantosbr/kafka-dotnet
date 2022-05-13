using Microsoft.AspNetCore.Mvc;

namespace Kafka.Consumer.WebApi.Controllers;

[ApiController]
[Route("home")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello World");
    }
}
