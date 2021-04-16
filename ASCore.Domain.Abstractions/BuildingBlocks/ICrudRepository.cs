using System;
using System.Threading;
using System.Threading.Tasks;

namespace ASCore.Domain.Abstractions.BuildingBlocks
{
    public interface ICrudRepository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        Task<TAggregateRoot> CreateAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
        Task<TAggregateRoot> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TAggregateRoot> UpdateAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
