using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{


    public class ClientTemperatureService
    {

  
        public int min { get; set; }
        public int max { get; set; }
    }


    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
     

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _service;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }


        [HttpPost("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> generateTemps([FromQuery] int count, [FromBody] ClientTemperatureService number)
        {
              if (number == null || count <= 0 || number.min > number.max)
            {
                return BadRequest("Invalid input parameters.");
            }
            var result = _service.Get(count, number.min,number.max);

            return Ok(result);

        }


    }
}
