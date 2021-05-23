using System.Reflection;
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
        private const string RabbitMQConnectionStringPattern = "amqp://{0}";
        private const string RabbitConnectionCheckName = "RabbitMQConnection";
        private const string DefaultRabbitMQTag = "RabbitMQBus";

        internal static IServiceCollection AddRabbitMQ(this IServiceCollection services,
            IConfiguration configuration,
            bool useHealthCheck,
            Assembly consumersAssembly)
        {
            var settingsSection = configuration.GetSection(RabbitMQSettingsSectionKey);
            var rabbitMQSettings = settingsSection.Get<RabbitMQSettings>();

            services
                .AddMassTransit(configurator =>
                {
                    configurator.AddConsumers(consumersAssembly);
                    configurator.SetKebabCaseEndpointNameFormatter();
                    configurator.UsingRabbitMq((context, busFactoryConfigurator) =>
                    {
                        busFactoryConfigurator
                            .Host(rabbitMQSettings.HostName,
                                rabbitMQSettings.VirtualHostName,
                                hostConfigurator =>
                                {
                                    hostConfigurator.Username(rabbitMQSettings.UserName);
                                    hostConfigurator.Password(rabbitMQSettings.Password);
                                });

                        busFactoryConfigurator.ConfigureEndpoints(context);
                    });
                })
                .AddMassTransitHostedService()
                .Configure<RabbitMQSettings>(settingsSection)
                .AddScoped<IIntegrationEventPublisher, EventBus>();

            if (useHealthCheck)
            {
                services
                    .AddHealthChecks()
                    .AddRabbitMQ(string.Format(RabbitMQConnectionStringPattern,
                            rabbitMQSettings.HostName),
                        name: RabbitConnectionCheckName,
                        tags: new[] { DefaultRabbitMQTag });
            }

            return services;
        }
    }
}
