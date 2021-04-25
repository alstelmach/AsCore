using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AsCore.Infrastructure.Mvc.Cors
{
    public static class CorsConfiguration
    {
        private const string AllowedOriginsSectionName = "AllowedOrigins";
        private const string PolicyName = "ApplicationCorsPolicy";

        internal static IServiceCollection RegisterCorsDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var origins = configuration
                .GetSection(AllowedOriginsSectionName)
                .Get<ICollection<string>>();

            return services
                .AddCors(options =>
                {
                    options.AddPolicy(
                        PolicyName,
                        config =>
                        {
                            foreach (var origin in origins)
                            {
                                config.WithOrigins(origin)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials();
                            }
                        });
                });
        }

        public static IApplicationBuilder UseCorsMiddlewares(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder
                .UseCors(PolicyName);
    }
}
