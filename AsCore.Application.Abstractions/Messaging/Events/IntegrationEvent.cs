using System;

namespace AsCore.Application.Abstractions.Messaging.Events
{
    public abstract record IntegrationEvent
    {
        protected IntegrationEvent(object entityId) =>
            EntityId = entityId;

        public Guid Id { get; } = Guid.NewGuid();
        public object EntityId { get; }
        public DateTime? PublishedAtUtc { get; set; }
    }
}
