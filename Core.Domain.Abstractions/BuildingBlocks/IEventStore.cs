using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Domain.Abstractions.BuildingBlocks
{
    public interface IEventStore
    {
        Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid id, CancellationToken cancellationToken = default)
            where TAggregateRoot : AggregateRoot;

        Task<IReadOnlyCollection<TAggregateRoot>> GetAsync<TAggregateRoot>(CancellationToken cancellationToken = default)
            where TAggregateRoot : AggregateRoot;

        Task ModifyAsync<TAggregateRoot>(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
            where TAggregateRoot : AggregateRoot;

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
