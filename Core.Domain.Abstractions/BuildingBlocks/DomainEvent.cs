using System;

namespace Core.Domain.Abstractions.BuildingBlocks
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreatedAtUtc { get; } = DateTime.UtcNow;
    }
}
