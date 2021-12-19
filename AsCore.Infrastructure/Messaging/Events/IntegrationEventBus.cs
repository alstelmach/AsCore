using System.Threading.Tasks;
using AsCore.Application.Abstractions.Messaging.Events;
using AsCore.Domain.Abstractions.Ports;
using MassTransit;

namespace AsCore.Infrastructure.Messaging.Events
{
    public sealed class IntegrationEventBus : IIntegrationEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IDateTimeProvider _dateTimeProvider;

        public IntegrationEventBus(
            IPublishEndpoint publishEndpoint,
            IDateTimeProvider dateTimeProvider)
        {
            _publishEndpoint = publishEndpoint;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent @event)
            where TIntegrationEvent : IntegrationEvent
        {
            @event.PublishedAtUtc = _dateTimeProvider.UtcNow;
            await _publishEndpoint.Publish(@event);
        }
    }
}
