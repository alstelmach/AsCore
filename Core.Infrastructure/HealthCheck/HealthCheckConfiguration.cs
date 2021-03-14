using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Core.Infrastructure.HealthCheck
{
    public static class HealthCheckConfiguration
    {
        private const string HealthChecksPath = "/hc";
        
        public static IApplicationBuilder UseHealthChecksMiddleware(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder
                .UseHealthChecks(HealthChecksPath, new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
    }
}
