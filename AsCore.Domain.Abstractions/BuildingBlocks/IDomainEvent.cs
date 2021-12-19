using MediatR;

namespace AsCore.Domain.Abstractions.BuildingBlocks
{
    public interface IDomainEvent : INotification 
    {
        public object AggregateRootId { get; }
    }
}
