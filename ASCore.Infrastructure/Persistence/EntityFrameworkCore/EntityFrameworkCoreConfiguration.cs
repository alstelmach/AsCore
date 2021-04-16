using System;
using ASCore.Infrastructure.Persistence.BuildingBlocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASCore.Infrastructure.Persistence.EntityFrameworkCore
{
    public static class EntityFrameworkCoreConfiguration
    {
        private const string SqlServerTag = "SqlServerConnection";
        private const string PostgreSQLTag = "PostgreSQLConnection";
        
        public static IServiceCollection AddDatabaseContext<TContext>(this IServiceCollection services,
            IConfiguration configuration,
            DatabaseProvider provider,
            bool useHealthCheck,
            string connectionStringSectionName,
            string migrationAssemblyName = null) where TContext : DbContext
        {
            var connectionString = configuration
                .GetConnectionString(connectionStringSectionName);
            
            var databaseContextOptionsAction = CreateContextOptions(provider,
                connectionString,
                migrationAssemblyName);
            
            return services
                .AddDbContext<TContext>(databaseContextOptionsAction)
                .ConfigureHealthChecks<TContext>(useHealthCheck,
                    provider,
                    connectionString);
        }
        
        public static IApplicationBuilder UseMigrationsOfContext<TContext>(this IApplicationBuilder applicationBuilder)
            where TContext : DbContext
        {
            using var serviceScope = applicationBuilder
                .ApplicationServices
                .CreateScope();

            var databaseContext = serviceScope
                .ServiceProvider
                .GetService<TContext>();

            var isInvalid = databaseContext is null;

            if (isInvalid)
            {
                var errorMessage =
                    $"{nameof(UseMigrationsOfContext)}: {typeof(TContext).FullName}";

                throw new ArgumentException(errorMessage);
            }

            databaseContext
                .Database
                .Migrate();

            return applicationBuilder;
        }

        private static Action<DbContextOptionsBuilder> CreateContextOptions(DatabaseProvider provider,
            string connectionString,
            string migrationAssemblyName = null) =>
                provider switch
                {
                    DatabaseProvider.SqlServer => migrationAssemblyName != null
                        ? (Action<DbContextOptionsBuilder>) (optionsBuilder => optionsBuilder
                            .UseSqlServer(connectionString,
                                sqlServerOptionsBuilder =>
                                    sqlServerOptionsBuilder.MigrationsAssembly(migrationAssemblyName)))
                        : (DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(connectionString),
                    DatabaseProvider.PostgreSQL => migrationAssemblyName != null
                        ? (Action<DbContextOptionsBuilder>) (optionsBuilder => optionsBuilder
                            .UseNpgsql(connectionString,
                                postgresOptionsBuilder =>
                                    postgresOptionsBuilder.MigrationsAssembly(migrationAssemblyName)))
                        : (DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(connectionString),
                    _ => throw new ArgumentOutOfRangeException(nameof(DatabaseProvider))
                };

        private static IServiceCollection ConfigureHealthChecks<TContext>(this IServiceCollection services,
            bool useHealthCheck,
            DatabaseProvider provider,
            string connectionString) where TContext : DbContext
        {
            if (!useHealthCheck)
            {
                return services;
            }

            return services
                .AddDatabaseHealthChecks<TContext>(
                    provider,
                    connectionString);
        }

        private static IServiceCollection AddDatabaseHealthChecks<TContext>(this IServiceCollection services,
            DatabaseProvider provider,
            string connectionString) where TContext : DbContext =>
                provider switch
                {
                    DatabaseProvider.SqlServer => services
                        .AddHealthChecks()
                        .AddSqlServer(connectionString,
                            name: typeof(TContext).FullName,
                            tags: new[] {SqlServerTag})
                        .Services,
                    DatabaseProvider.PostgreSQL => services
                        .AddHealthChecks()
                        .AddNpgSql(connectionString,
                            name: typeof(TContext).FullName,
                            tags: new[] {PostgreSQLTag})
                        .Services,
                    _ => throw new ArgumentOutOfRangeException(nameof(DatabaseProvider))
                };
    }
}
