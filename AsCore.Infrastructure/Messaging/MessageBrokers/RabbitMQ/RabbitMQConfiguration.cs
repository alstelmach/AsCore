using AsCore.Application.Abstractions.Messaging.Events;
using AsCore.Infrastructure.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AsCore.Infrastructure.Messaging.MessageBrokers.RabbitMQ
{
    internal static class RabbitMQConfiguration
    {
        private const string RabbitMQSettingsSectionKey = "Messaging:RabbitMQ";

        internal static IServiceCollection RegisterRabbitMQDependencies(
            this IServiceCollection services,
            IConfiguration configuration,
            string exchangeType)
        {
            var rabbitMQSettings = configuration
                .GetSection(RabbitMQSettingsSectionKey)
                .Get<RabbitMQSettings>();

            services
                .AddMassTransit(configurator =>
                {
                    configurator.AddConsumers(typeof(IntegrationEventHandler<IntegrationEvent>).Assembly);
                })
                .AddSingleton(serviceProvider => MassTransit.Bus.Factory.CreateUsingRabbitMq(configurator =>
                {
                    configurator
                        .Host(rabbitMQSettings.HostName,
                            rabbitMQSettings.VirtualHostName,
                            hostConfigurator =>
                            {
                                hostConfigurator.Username(rabbitMQSettings.UserName);
                                hostConfigurator.Password(rabbitMQSettings.Password);
                            });

                    configurator.ExchangeType = exchangeType;
                }))
                .AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>())
                .AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>())
                .AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>())
                .Configure<RabbitMQSettings>(configuration.GetSection(RabbitMQSettingsSectionKey));

            return services;
        }
    }
}
