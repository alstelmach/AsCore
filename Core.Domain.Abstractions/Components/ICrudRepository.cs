using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Domain.Abstractions.Components
{
    public interface ICrudRepository<TAggregateRoot> : IRepository
        where TAggregateRoot : IAggregateRoot
    {
        Task<TAggregateRoot> CreateAsync(TAggregateRoot aggregateRoot);
        Task<IEnumerable<TAggregateRoot>> GetAsync(CancellationToken cancellationToken = default);
        Task<TAggregateRoot> GetAsync(Guid id, CancellationToken cancellationToken = default);
        void Update(TAggregateRoot aggregateRoot);
        Task DeleteAsync(Guid id);
    }
}
