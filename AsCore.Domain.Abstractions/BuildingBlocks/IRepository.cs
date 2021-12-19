using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AsCore.Domain.Abstractions.BuildingBlocks
{
    public interface IRepository<TEntity, in TKey> where TEntity : IIdentifiable<TKey>
    {
        Task CreateAsync(
            TEntity entity,
            CancellationToken cancellationToken = default);

        Task CreateManyAsync(
            ICollection<TEntity> entity,
            CancellationToken cancellationToken = default);

        Task<ICollection<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filterExpression,
            CancellationToken cancellationToken = default);

        Task<ICollection<TEntity>> GetAsync(
            CancellationToken cancellationToken = default);

        Task<TEntity> GetAsync(
            TKey key,
            CancellationToken cancellationToken = default);

        Task UpdateAsync(
            TEntity entity,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            TKey key,
            CancellationToken cancellationToken = default);

        Task CommitAsync(
            CancellationToken cancellationToken = default);
    }
}
