using System;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace AsCore.Infrastructure.HealthCheck
{
    public static class HealthCheckConfiguration
    {
        private const string HealthChecksPath = "/hc";
        
        // ToDo: remove
        [Obsolete("To be removed from this package")]
        public static IApplicationBuilder UseHealthChecksMiddleware(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder
                .UseHealthChecks(HealthChecksPath, new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
    }
}
