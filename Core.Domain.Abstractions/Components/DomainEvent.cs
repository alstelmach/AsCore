using System;

namespace Core.Domain.Abstractions.Components
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreatedAtUtc { get; } = DateTime.UtcNow;
    }
}
