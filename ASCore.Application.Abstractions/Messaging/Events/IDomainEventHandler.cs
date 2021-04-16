using ASCore.Domain.Abstractions.BuildingBlocks;
using MediatR;

namespace ASCore.Application.Abstractions.Messaging.Events
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
    {
    }
}
