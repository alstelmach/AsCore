using System;

namespace Core.Application.Abstractions.Messaging.Events
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreatedAtUtc { get; } = DateTime.UtcNow;
        public DateTime? PublishedAtUtc { get; set; }
    }
}
