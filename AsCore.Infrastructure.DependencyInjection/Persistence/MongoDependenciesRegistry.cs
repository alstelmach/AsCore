using System.Security.Authentication;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace AsCore.Infrastructure.DependencyInjection.Persistence;

public static class MongoDependenciesRegistry
{
    private static IServiceCollection AddMongoDependencies(
        this IServiceCollection services,
        string connectionString,
        int enabledSslProtocol)
    {
        var mongoSettings = MongoClientSettings.FromConnectionString(connectionString);

        mongoSettings.SslSettings.EnabledSslProtocols = (SslProtocols) enabledSslProtocol;

        var mongoClient = new MongoClient(mongoSettings);

        services.AddSingleton<IMongoClient>(mongoClient);

        return services;
    }
}
