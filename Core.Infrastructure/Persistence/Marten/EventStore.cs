using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Abstractions.BuildingBlocks;
using Marten;

namespace Core.Infrastructure.Persistence.Marten
{
    public sealed class EventStore : IEventStore
    {
        private readonly IDocumentSession _session;
        private readonly IDomainEventPublisher _domainEventPublisher;
        
        public EventStore(IDocumentStore documentStore,
            IDomainEventPublisher domainEventPublisher)
        {
            _session = documentStore.OpenSession();
            _domainEventPublisher = domainEventPublisher;
        }

        public async Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid id,
            CancellationToken cancellationToken = default) where TAggregateRoot : AggregateRoot =>
                await _session
                    .Events
                    .AggregateStreamAsync<TAggregateRoot>(id, token: cancellationToken);

        public async Task<IReadOnlyCollection<TAggregateRoot>> GetAsync<TAggregateRoot>(
            CancellationToken cancellationToken = default)
            where TAggregateRoot : AggregateRoot =>
                await _session
                    .Query<TAggregateRoot>()
                    .ToListAsync(cancellationToken);

        public async Task ModifyAsync<TAggregateRoot>(TAggregateRoot aggregateRoot,
            CancellationToken cancellationToken = default) where TAggregateRoot : AggregateRoot
        {
            var events = aggregateRoot.DequeueDomainEvents();

            _session
                .Events
                .Append(aggregateRoot.Id,
                    events);

            await _session.SaveChangesAsync(cancellationToken);
            await _domainEventPublisher.PublishAsync(events);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
            await Task.Run(() =>
                _session.Delete(id), cancellationToken);
    }
}
