using JsonNet.PrivatePropertySetterResolver;
using Marten;
using Marten.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Core.Infrastructure.Persistence.PostgreSQL.Marten
{
    public static class MartenConfiguration
    {
        private const string DefaultPostgreSqlTag = "PostgreSQL";
        
        public static IServiceCollection AddMarten(this IServiceCollection services,
            IConfiguration configuration,
            string schemaName,
            string connectionStringSectionName,
            bool useHealthCheck = default,
            string healthCheckName = default)
        {
            var connectionString = configuration
                .GetConnectionString(connectionStringSectionName);

            services
                .AddSingleton(DocumentStore.For(options =>
                {
                    options.Connection(connectionString);
                    options.DatabaseSchemaName = schemaName;
                    options.Serializer(GetCustomJsonSerializer());
                }));

            if (useHealthCheck)
            {
                services
                    .AddHealthChecks()
                    .AddNpgSql(connectionString,
                        name: healthCheckName,
                        tags: new[] { DefaultPostgreSqlTag });
            }

            return services;
        }

        private static JsonNetSerializer GetCustomJsonSerializer()
        {
            var serializer = new JsonNetSerializer();
            
            serializer
                .Customize(configuration =>
                {
                    configuration.ContractResolver = new PrivatePropertySetterResolver(); // ToDo: check it out
                    configuration.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                });

            return serializer;
        }
    }
}
