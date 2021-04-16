using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASCore.Domain.Abstractions.BuildingBlocks
{
    public interface IEventStore
    {
        Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid id, CancellationToken cancellationToken = default)
            where TAggregateRoot : AggregateRoot;

        Task ModifyAsync<TAggregateRoot>(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
            where TAggregateRoot : AggregateRoot;

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<IDomainEvent>> GetDomainEventsAsync(Guid streamId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<TEvent>> GetDomainEventsAsync<TEvent>(CancellationToken cancellationToken = default);
    }
}
