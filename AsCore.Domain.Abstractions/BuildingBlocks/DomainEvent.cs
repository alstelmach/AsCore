using System;

namespace AsCore.Domain.Abstractions.BuildingBlocks
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent(Guid entityId)
        {
            EntityId = entityId;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public Guid EntityId { get; }
        public DateTime CreatedAtUtc { get; } = DateTime.UtcNow;
    }
}
