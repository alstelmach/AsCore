﻿using ASCore.Application.Abstractions.Messaging.Commands;
using ASCore.Application.Abstractions.Messaging.Events;
using ASCore.Application.Abstractions.Messaging.Queries;
using ASCore.Domain.Abstractions.BuildingBlocks;
using ASCore.Infrastructure.Messaging.Commands;
using ASCore.Infrastructure.Messaging.Events;
using ASCore.Infrastructure.Messaging.MessageBrokers.RabbitMQ;
using ASCore.Infrastructure.Messaging.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASCore.Infrastructure.Messaging
{
    public static class MessagingDependenciesRegistry
    {
        private const string RabbitMQSettingsSectionKey = "Messaging:RabbitMQ";
        private const string RabbitMQConnectionStringPattern = "amqp://{0}";
        private const string RabbitConnectionCheckName = "RabbitMQConnection";
        private const string DefaultRabbitMQTag = "RabbitMQBus";

        public static IServiceCollection AddMessaging(this IServiceCollection services) =>
            services
                .AddScoped<IMediator, Mediator>()
                .AddScoped<ServiceFactory>(serviceProvider =>
                    serviceProvider.GetService)
                .AddScoped<ICommandBus, CommandBus>()
                .AddScoped<IQueryBus, QueryBus>()
                .AddScoped<IDomainEventPublisher, EventBus>();

        public static IServiceCollection AddRabbitMQ(this IServiceCollection services,
            IConfiguration configuration,
            string exchangeType,
            bool useHealthCheck)
        {
            var rabbitSettings = configuration
                .GetSection(RabbitMQSettingsSectionKey)
                .Get<RabbitMQSettings>();
            
            services
                .RegisterRabbitMQDependencies(configuration,
                    exchangeType)
                .AddScoped<IIntegrationEventPublisher, EventBus>();

            if (useHealthCheck)
            {
                services
                    .AddHealthChecks()
                    .AddRabbitMQ(string.Format(RabbitMQConnectionStringPattern,
                            rabbitSettings.HostName),
                        name: RabbitConnectionCheckName,
                        tags: new[] {DefaultRabbitMQTag,});
            }

            return services;
        }
    }
}