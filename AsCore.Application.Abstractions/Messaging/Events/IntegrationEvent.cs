using System;

namespace AsCore.Application.Abstractions.Messaging.Events
{
    public abstract record IntegrationEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime? PublishedAtUtc { get; set; }
    }
}
