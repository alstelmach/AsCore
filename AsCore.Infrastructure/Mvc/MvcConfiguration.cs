using AsCore.Infrastructure.Mvc.Cors;
using AsCore.Infrastructure.Mvc.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AsCore.Infrastructure.Mvc
{
    public static class MvcConfiguration
    {
        public static IServiceCollection AddMvcDependencies(this IServiceCollection services,
            IConfiguration configuration,
            int majorVersion,
            int minorVersion,
            bool addCors = false)
        {
            services
                .AddControllers(options => options
                    .Filters
                    .Add<ExceptionFilter>())
                .AddNewtonsoftJson(options =>
                    options
                        .SerializerSettings
                        .ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .Services
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(majorVersion, minorVersion);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                });

            return addCors
                ? services.RegisterCorsDependencies(configuration)
                : services;
        }

        public static IApplicationBuilder UseMvcMiddlewares(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder
                .UseEndpoints(builder =>
                    builder.MapControllers());
    }
}
