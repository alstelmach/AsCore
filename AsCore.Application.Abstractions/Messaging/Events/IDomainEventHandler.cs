using AsCore.Domain.Abstractions.BuildingBlocks;
using MediatR;

namespace AsCore.Application.Abstractions.Messaging.Events
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
    {
    }
}
