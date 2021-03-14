using System;
using MediatR;

namespace Core.Domain.Abstractions.BuildingBlocks
{
    public interface IDomainEvent : INotification,
        IIdentifiable
    {
        DateTime CreatedAtUtc { get; }        
    }
}
