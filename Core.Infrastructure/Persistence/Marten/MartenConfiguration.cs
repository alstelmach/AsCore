using JsonNet.PrivatePropertySetterResolver;
using Marten;
using Marten.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Core.Infrastructure.Persistence.Marten
{
    public static class MartenConfiguration
    {
        private const string DefaultPostgreSqlTag = "PostgreSQL";
        private const string EventStoreSectionKey = "EventStoreSettings";
        
        public static IServiceCollection AddMarten(this IServiceCollection services,
            IConfiguration configuration,
            string schemaName,
            string connectionStringSectionName,
            bool useHealthCheck = default,
            string healthCheckName = default)
        {
            var connectionString = configuration
                .GetConnectionString(connectionStringSectionName);

            var eventStoreSettings = services
                .Configure<EventStoreSettings>(configuration
                    .GetSection(EventStoreSectionKey))
                .BuildServiceProvider()
                .GetRequiredService<IOptions<EventStoreSettings>>()
                .Value;

            services
                .AddSingleton<IDocumentStore>(DocumentStore.For(options =>
                {
                    options.Connection(connectionString);
                    options.DatabaseSchemaName = schemaName;
                    options.Serializer(GetCustomJsonSerializer());
                    
                    options.CreateDatabasesForTenants(databaseConfig =>
                    {
                        databaseConfig.MaintenanceDatabase(connectionString);
                        
                        // ToDo: Does not work - solve

                        databaseConfig
                            .ForTenant()
                            .CheckAgainstPgDatabase()
                            .WithOwner(eventStoreSettings.DatabaseOwner)
                            .WithEncoding(eventStoreSettings.Encoding)
                            .ConnectionLimit(eventStoreSettings.ConnectionLimit);
                    });
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
                    configuration.ContractResolver = new PrivatePropertySetterResolver();
                    configuration.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                });

            return serializer;
        }
    }
}
