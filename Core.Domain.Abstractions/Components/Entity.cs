using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Domain.Abstractions.Components
{
    public abstract class Entity : IIdentifiable
    {
        protected Entity(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; }
        
        protected readonly ICollection<IDomainEvent> Events = new List<IDomainEvent>();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => Events
            .ToList()
            .AsReadOnly();

        public void ClearDomainEvents() =>
            Events.Clear();
    }
}
