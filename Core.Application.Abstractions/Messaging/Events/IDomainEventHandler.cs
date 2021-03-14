using Core.Domain.Abstractions.BuildingBlocks;
using MediatR;

namespace Core.Application.Abstractions.Messaging.Events
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
    {
    }
}
