using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Helpers
{
    public class ExternalServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceSetting _serviceSetting;

        public ExternalServiceClient(HttpClient httpClient, IOptions<ServiceSetting> option)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _serviceSetting = option.Value;

            
        }
        public record Weather(string description);

        public record Main(decimal temp);

        public record Forecast(Weather[] weather, Main main, long dt);

        public async Task<Forecast> GetForecastAsync(string city)
        {
            return await _httpClient.GetFromJsonAsync<Forecast>($"https://{_serviceSetting.OpenWeatherHost}/data/2.5/weather?q={city}&appid={_serviceSetting.ApiKey}");

        }
    }
}
