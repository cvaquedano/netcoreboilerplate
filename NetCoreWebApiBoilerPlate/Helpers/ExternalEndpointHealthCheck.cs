using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Helpers
{
    public class ExternalEndpointHealthCheck : IHealthCheck
    {
        private readonly ServiceSetting _serviceSetting;
        public ExternalEndpointHealthCheck(IOptions<ServiceSetting> option)
        {
            _serviceSetting = option.Value;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping ping = new();
            var reply = await ping.SendPingAsync(_serviceSetting.OpenWeatherHost);
            if (reply.Status != IPStatus.Success)
            {
                return HealthCheckResult.Unhealthy();
            }
            return HealthCheckResult.Healthy();
        }
    }
}
