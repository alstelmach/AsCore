using System;

namespace AsCore.Application.Abstractions.Messaging.Events
{
    public abstract class IntegrationEvent
    {
        protected IntegrationEvent(Guid entityId,
            string eventType)
        {
            EntityId = entityId;
            EventType = eventType;
        }
        
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreatedAtUtc { get; } = DateTime.UtcNow;
        public Guid EntityId { get; }
        public string EventType { get; }
        public DateTime? PublishedAtUtc { get; set; }
    }
}
