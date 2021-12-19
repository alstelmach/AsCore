using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AsCore.Utilities.Swagger
{
    public static class SwaggerConfiguration
    {
        private const string NameSectionKey = "Utility:Swagger:Name";
        private const string VersionSectionString = "Utility:Swagger:Version";

        private const string SwaggerUrlTemplate = "/swagger/{0}/swagger.json";
        private const string AuthorizationHeaderKey = "Authorization";
        private const string AuthorizationHeaderDescription = "JWT Authorization header used in the bearer schema";
        private const string BearerAuthenticationName = "Bearer";

        // ToDo: Pass name and version as a param in here
        public static IServiceCollection AddSwagger(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var name = configuration.GetValue<string>(NameSectionKey);
            var version = configuration.GetValue<string>(VersionSectionString);

            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(
                        version,
                        new OpenApiInfo
                        {
                            Title = name,
                            Version = version,
                        });

                    options.AddSecurityDefinition(
                        BearerAuthenticationName,
                        new OpenApiSecurityScheme
                        {
                            Description = AuthorizationHeaderDescription,
                            Name = AuthorizationHeaderKey,
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = BearerAuthenticationName,
                        });

                    options.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = BearerAuthenticationName,
                                    },
                                },
                                Array.Empty<string>()
                            },
                        });

                    options.CustomSchemaIds(type => type.FullName);
                });

            return services;
        }

        public static IApplicationBuilder UseSwaggerMiddleware(
            this IApplicationBuilder applicationBuilder,
            IConfiguration configuration)
        {
            var swaggerUrl = string.Format(
                SwaggerUrlTemplate,
                configuration.GetValue<string>(VersionSectionString));

            var name = configuration.GetValue<string>(NameSectionKey);

            applicationBuilder
                .UseSwagger()
                .UseSwaggerUI(
                    options =>
                    {
                        options.SwaggerEndpoint(swaggerUrl, name);
                        options.RoutePrefix = string.Empty;
                    });

            return applicationBuilder;
        }
    }
}
