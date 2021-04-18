using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreWebApiBoilerPlate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NetCoreWebApiBoilerPlate.Helpers.ExternalServiceClient;

namespace NetCoreWebApiBoilerPlate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalServicesController : ControllerBase
    {
        private readonly ILogger<ExternalServicesController> _logger;
        private readonly ExternalServiceClient _externalServiceClient;

        public ExternalServicesController(ILogger<ExternalServicesController> logger, ExternalServiceClient externalServiceClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _externalServiceClient = externalServiceClient ?? throw new ArgumentNullException(nameof(externalServiceClient));
        }

        [HttpGet("{city}", Name = "GetweatherByCity")]
        [ProducesResponseType(typeof(Forecast), StatusCodes.Status200OK)]
        public async Task<ActionResult<WeatherForecast>> GetweatherByCity(string city)
        {
            var forecast = await _externalServiceClient.GetForecastAsync(city);

            return new WeatherForecast
            {
                Summary = forecast.weather[0].description,
                Date = DateTimeOffset.FromUnixTimeSeconds(forecast.dt).DateTime,
                TemperatureC = (int)forecast.main.temp

            };
        }

    }
}
