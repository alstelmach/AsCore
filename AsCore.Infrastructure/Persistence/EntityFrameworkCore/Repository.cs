using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsCore.Domain.Abstractions.BuildingBlocks;
using Microsoft.EntityFrameworkCore;

namespace AsCore.Infrastructure.Persistence.EntityFrameworkCore
{
    public abstract class Repository<TAggregateRoot> : ICrudRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        protected Repository(DbContext dbContext,
            IDomainEventPublisher domainEventPublisher)
        {
            DbContext = dbContext;
            DomainEventPublisher = domainEventPublisher;
        }

        protected DbContext DbContext { get; }
        protected IDomainEventPublisher DomainEventPublisher { get; }

        public virtual async Task<TAggregateRoot> CreateAsync(TAggregateRoot aggregateRoot,
            CancellationToken cancellationToken = default)
        {
            var root = (await DbContext
                .Set<TAggregateRoot>()
                .AddAsync(aggregateRoot, cancellationToken))
                .Entity;

            await CommitAsync(root);

            return root;
        }

        public virtual async Task<IEnumerable<TAggregateRoot>> GetAsync(CancellationToken cancellationToken = default) =>
            await DbContext
                .Set<TAggregateRoot>()
                .ToListAsync(cancellationToken);

        public virtual async Task<TAggregateRoot> GetAsync(Guid id, CancellationToken cancellationToken = default) =>
            await DbContext
                .Set<TAggregateRoot>()
                .FirstOrDefaultAsync(aggregateRoot =>
                    aggregateRoot.Id == id, cancellationToken);

        public virtual async Task<TAggregateRoot> UpdateAsync(TAggregateRoot aggregateRoot,
            CancellationToken cancellationToken = default)
        {
            DbContext
                .Set<TAggregateRoot>()
                .Update(aggregateRoot);

            await CommitAsync(aggregateRoot);
            
            return aggregateRoot;
        }
        
        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var aggregateRoot = await GetAsync(id, cancellationToken);
            var doesExist = aggregateRoot is not null;

            if (doesExist)
            {
                DbContext
                    .Set<TAggregateRoot>()
                    .Remove(aggregateRoot);
            }

            await CommitAsync(aggregateRoot);
        }
        
        protected async Task CommitAsync(TAggregateRoot aggregateRoot)
        {
            await DbContext.SaveChangesAsync();

            var domainEvents = aggregateRoot.DequeueDomainEvents();
            
            if (domainEvents.Any())
            {
                await DomainEventPublisher.PublishAsync(domainEvents);
            }
        }
    }
}
