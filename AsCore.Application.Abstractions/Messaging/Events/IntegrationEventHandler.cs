using System.Threading.Tasks;
using MassTransit;

namespace AsCore.Application.Abstractions.Messaging.Events
{
    public abstract class IntegrationEventHandler<TIntegrationEvent> : IConsumer<TIntegrationEvent>
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
