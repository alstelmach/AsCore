using System.Threading.Tasks;
using AsCore.Application.Abstractions.Messaging.Events;
using MassTransit;

namespace AsCore.Infrastructure.Messaging.Events
{
    public sealed class IntegrationEventBus : IIntegrationEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public IntegrationEventBus(IPublishEndpoint publishEndpoint) =>
            _publishEndpoint = publishEndpoint;

        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent @event)
            where TIntegrationEvent : IntegrationEvent
        {
            @event.MarkAsPublished();
            await _publishEndpoint.Publish(@event);
        }
    }
}
