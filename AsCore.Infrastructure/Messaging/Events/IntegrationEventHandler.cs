using System.Threading.Tasks;
using AsCore.Application.Abstractions.Messaging.Events;
using MassTransit;

namespace AsCore.Infrastructure.Messaging.Events
{
    public abstract class IntegrationEventHandler<TIntegrationEvent> : IIntegrationEventHandler<TIntegrationEvent>,
        IConsumer<TIntegrationEvent>
        where TIntegrationEvent : IntegrationEvent
    {
        protected ConsumeContext<TIntegrationEvent> ConsumeContext { get; private set; }

        public async Task Consume(ConsumeContext<TIntegrationEvent> context)
        {
            ConsumeContext = context;
            await HandleAsync(context.Message);
        }

        public abstract Task HandleAsync(TIntegrationEvent @event);
    }
}
