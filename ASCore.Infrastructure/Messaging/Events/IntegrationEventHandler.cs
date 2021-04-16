﻿using System.Threading.Tasks;
using ASCore.Application.Abstractions.Messaging.Events;
using MassTransit;

namespace ASCore.Infrastructure.Messaging.Events
{
    public abstract class IntegrationEventHandler<TIntegrationEvent> : IIntegrationEventHandler<TIntegrationEvent>,
        IConsumer<TIntegrationEvent>
            where TIntegrationEvent : IntegrationEvent
    {
        public async Task Consume(ConsumeContext<TIntegrationEvent> context) =>
            await HandleAsync(context.Message);

        public abstract Task HandleAsync(TIntegrationEvent @event);
    }
}