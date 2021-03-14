using System;
using System.Linq;

namespace Core.Domain.Abstractions.BuildingBlocks
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot(Guid id)
            : base(id)
        {
        }
        
        public int Version { get; private set; }
        
        // ToDo: Verify is it working, the main purpose is to raise version to provide transactions within an aggregate using version (raised if domain events occured)
        public void IncrementVersionIfNeeded()
        {
            var type = GetType();
            
            var domainEvents = type
                .GetFields()
                .Where(field =>
                    field.FieldType.IsSubclassOf(typeof(Entity)))
                .Select(field => (field.GetValue(this) as Entity)?.DomainEvents)
                .SelectMany(collection => collection)
                .ToList();

            domainEvents
                .AddRange(type
                    .GetProperties()
                    .Where(property =>
                        property.PropertyType.IsSubclassOf(typeof(Entity)))
                    .Select(field => (field.GetValue(this) as Entity)?.DomainEvents)
                    .SelectMany(collection => collection)
                    .ToList());

            if (domainEvents.Any())
            {
                Version++;
            }
        }
    }
}
