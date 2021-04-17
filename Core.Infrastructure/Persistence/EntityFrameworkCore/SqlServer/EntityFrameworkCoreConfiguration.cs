using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Persistence.EntityFrameworkCore.SqlServer
{
    public static class EntityFrameworkCoreConfiguration
    {
        private const string DefaultSqlServerTag = "SqlServerConnection";

        public static IServiceCollection AddDatabaseContext<TContext>(this IServiceCollection services,
            IConfiguration configuration,
            bool useHealthCheck,
            string connectionStringSectionName,
            string migrationAssemblyName = null) where TContext : DbContext
        {
            var connectionString = configuration
                .GetConnectionString(connectionStringSectionName);

            var databaseContextOptionsAction = migrationAssemblyName != null
                ? (Action<DbContextOptionsBuilder>) (optionsBuilder => optionsBuilder
                    .UseSqlServer(connectionString,
                        sqlServerOptionsBuilder =>
                            sqlServerOptionsBuilder.MigrationsAssembly(migrationAssemblyName)))
                : optionsBuilder => optionsBuilder.UseSqlServer(connectionString);

            services
                .AddDbContext<TContext>(databaseContextOptionsAction);

            if (useHealthCheck)
            {
                services
                    .AddHealthChecks()
                    .AddSqlServer(connectionString,
                        name: typeof(TContext).FullName,
                        tags: new[] { DefaultSqlServerTag });
            }

            return services;
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
    }
}
