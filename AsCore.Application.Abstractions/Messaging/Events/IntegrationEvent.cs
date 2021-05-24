using System;

namespace AsCore.Application.Abstractions.Messaging.Events
{
    public abstract record IntegrationEvent
    {
        protected IntegrationEvent(Guid entityId,
            string eventType)
        {
            EntityId = entityId;
            EventType = eventType;
        }
        
        public Guid Id { get; } = Guid.NewGuid();
        public Guid EntityId { get; }
        public string EventType { get; }
        public DateTime CreatedAtUtc { get; } = DateTime.UtcNow;
        public DateTime? PublishedAtUtc { get; private set; }

        public virtual void MarkAsPublished() =>
            PublishedAtUtc = DateTime.UtcNow;
    }
}
