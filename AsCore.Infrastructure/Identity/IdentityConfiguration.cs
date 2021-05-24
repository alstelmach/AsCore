using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AsCore.Infrastructure.Identity
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection RegisterIdentityDependencies(this IServiceCollection services,
            byte[] secretKey) =>
                services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            LifetimeValidator = ValidateLifetime,
                            ClockSkew = TimeSpan.Zero
                        };
                    })
                    .Services;

        public static IApplicationBuilder UseIdentityMiddlewares(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder
                .UseAuthentication()
                .UseAuthorization();

        private static bool ValidateLifetime(DateTime? notBeforeTime,
            DateTime? expirationTime,
            SecurityToken token,
            TokenValidationParameters validationParams) =>
                expirationTime is not null
                    && expirationTime > DateTime.UtcNow;
    }
}
