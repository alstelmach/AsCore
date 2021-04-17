using System;
using System.Threading;
using System.Threading.Tasks;
using AsCore.Domain.Abstractions.BuildingBlocks;

namespace AsCore.Infrastructure.Persistence.Marten
{
    public abstract class Repository<TAggregateRoot> : ICrudRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        protected Repository(IEventStore eventStore) =>
            EventStore = eventStore;

        protected IEventStore EventStore { get; }

        public async Task<TAggregateRoot> CreateAsync(TAggregateRoot aggregateRoot,
            CancellationToken cancellationToken = default) =>
                await ModifyAsync(aggregateRoot, cancellationToken);

        public async Task<TAggregateRoot> GetAsync(Guid id, CancellationToken cancellationToken = default) =>
            await EventStore
                .FindAsync<TAggregateRoot>(id, cancellationToken);

        public async Task<TAggregateRoot> UpdateAsync(TAggregateRoot aggregateRoot,
            CancellationToken cancellationToken = default) =>
                await ModifyAsync(aggregateRoot, cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
            await EventStore
                .DeleteAsync(id, cancellationToken);

        protected async Task<TAggregateRoot> ModifyAsync(TAggregateRoot aggregateRoot,
            CancellationToken cancellationToken)
        {
            await EventStore.ModifyAsync(aggregateRoot, cancellationToken);
            return aggregateRoot;
        }
    }
}