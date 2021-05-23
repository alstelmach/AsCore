using System;
using System.Reflection;
using AsCore.Application.Abstractions.Messaging.Commands;
using AsCore.Application.Abstractions.Messaging.Queries;
using AsCore.Domain.Abstractions.BuildingBlocks;
using AsCore.Infrastructure.Messaging.Commands;
using AsCore.Infrastructure.Messaging.Events;
using AsCore.Infrastructure.Messaging.MessageBrokers;
using AsCore.Infrastructure.Messaging.MessageBrokers.RabbitMQ;
using AsCore.Infrastructure.Messaging.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AsCore.Infrastructure.Messaging
{
    public static class MessagingDependenciesRegistry
    {
        public static IServiceCollection AddDomesticMessaging(this IServiceCollection services) =>
            services
                .AddScoped<IMediator, Mediator>()
                .AddScoped<ServiceFactory>(serviceProvider =>
                    serviceProvider.GetService)
                .AddScoped<ICommandBus, CommandBus>()
                .AddScoped<IQueryBus, QueryBus>()
                .AddScoped<IDomainEventPublisher, EventBus>();

        public static IServiceCollection AddIntegrationMessaging(this IServiceCollection services,
            IConfiguration configuration,
            MessageBroker messageBroker,
            bool useHealthCheck,
            Assembly consumersAssembly) =>
                messageBroker switch
                {
                    MessageBroker.RabbitMQ => services.AddRabbitMQ(
                        configuration,
                        useHealthCheck,
                        consumersAssembly),
                    _ => throw new ArgumentOutOfRangeException(nameof(MessageBroker))
                };
    }
}
