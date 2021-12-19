using System;

namespace AsCore.Domain.Abstractions.BuildingBlocks
{
    public abstract record DomainEvent(object AggregateRootId) : IDomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
