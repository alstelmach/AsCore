using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Abstractions.BuildingBlocks;
using Core.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Infrastructure.Persistence.EntityFrameworkCore
{
    internal static class DbContextExtension
    {
        internal static ICollection<EntityEntry<Entity>> GetAffectedTrackedEntities(
            this DbContext dbContext) =>
                dbContext
                    .ChangeTracker
                    .Entries<Entity>()
                    .Where(entityEntry =>
                        entityEntry.State != EntityState.Unchanged)
                    .ToList();

        internal static async Task DispatchDomainEventsAsync(this DbContext dbContext,
            IDomainEventPublisher domainEventPublisher,
            ICollection<EntityEntry<Entity>> entities)
        {
            var domainEventsPublicationTasks = entities
                .SelectMany(entityEntry => entityEntry.Entity.DomainEvents)
                .Select(@event => domainEventPublisher.PublishAsync(@event))
                .ToList();

            await Task.WhenAll(domainEventsPublicationTasks);
            
            entities
                .ForEach(entityEntry => entityEntry.Entity.ClearDomainEvents());
        }
    }
}
