using Core.Infrastructure.Mvc.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Core.Infrastructure.Mvc
{
    public static class MvcConfiguration
    {
        public static IServiceCollection AddMvcDependencies(this IServiceCollection services) =>
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
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                });

        public static IApplicationBuilder UseEndpointsMiddleware(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder
                .UseEndpoints(builder =>
                    builder.MapControllers());
    }
}
