using System.Linq;
using System.Threading.Tasks;
using AsCore.Application.Abstractions.Messaging.Events;
using AsCore.Domain.Abstractions.BuildingBlocks;
using MassTransit;
using MediatR;

namespace AsCore.Infrastructure.Messaging.Events
{
    public sealed class EventBus : Bus,
        IDomainEventPublisher,
        IIntegrationEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBus(IMediator mediator,
            IPublishEndpoint publishEndpoint)
                : base(mediator) =>
                    _publishEndpoint = publishEndpoint;

        public async Task PublishAsync(params IDomainEvent[] events)
        {
            var publicationTasks = events
                .Select(@event => Mediator.Publish(@event));

            await Task.WhenAll(publicationTasks);
        }

        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent @event)
            where TIntegrationEvent : IntegrationEvent
        {
            @event.MarkAsPublished();
            await _publishEndpoint.Publish(@event);
        }
    }
}
