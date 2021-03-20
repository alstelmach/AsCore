using System;

namespace Core.Domain.Abstractions.BuildingBlocks
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent(Guid entityId)
        {
            EntityId = entityId;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreatedAtUtc { get; } = DateTime.UtcNow;
        public Guid EntityId { get; }
    }
}
