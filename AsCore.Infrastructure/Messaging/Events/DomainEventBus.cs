using System.Linq;
using System.Threading.Tasks;
using AsCore.Domain.Abstractions.BuildingBlocks;
using MediatR;

namespace AsCore.Infrastructure.Messaging.Events
{
    public sealed class DomainEventBus : Bus, IDomainEventPublisher
    {
        public DomainEventBus(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task PublishAsync(params IDomainEvent[] events)
        {
            var publicationTasks = events
                .Select(@event => Mediator.Publish(@event));

            await Task.WhenAll(publicationTasks);
        }
    }
}
