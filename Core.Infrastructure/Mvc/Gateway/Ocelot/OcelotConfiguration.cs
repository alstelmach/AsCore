using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Core.Infrastructure.Mvc.Gateway.Ocelot
{
    public static class OcelotConfiguration
    {
        public static IServiceCollection AddOcelotGateway(this IServiceCollection services) =>
            services
                .AddOcelot()
                .AddCacheManager(builder => builder.WithDictionaryHandle())
                .Services;

        public static void UseOcelotMiddleware(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder
                .UseOcelot()
                .Wait();
    }
}
