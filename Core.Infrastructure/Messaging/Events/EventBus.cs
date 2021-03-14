using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Abstractions.Messaging.Events;
using Core.Domain.Abstractions.BuildingBlocks;
using MassTransit;
using MediatR;

namespace Core.Infrastructure.Messaging.Events
{
    public sealed class EventBus : Bus,
        IDomainEventPublisher,
        IIntegrationEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBus(IMediator mediator,
            IPublishEndpoint publishEndpoint)
            : base(mediator)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync(params IDomainEvent[] events)
        {
            var publicationTasks = events
                .Select(@event => Mediator.Publish(@event));

            await Task.WhenAll(publicationTasks);
        }

        public async Task PublishAsync(params IntegrationEvent[] events)
        {
            var globalPublicationTasks = events
                .Select(@event =>
                {
                    @event.PublishedAtUtc = DateTime.UtcNow;

                    return _publishEndpoint.Publish(@event);
                });

            await Task.WhenAll(globalPublicationTasks);
        }
    }
}
