using System;
using MediatR;

namespace ASCore.Domain.Abstractions.BuildingBlocks
{
    public interface IDomainEvent : INotification,
        IIdentifiable
    {
        DateTime CreatedAtUtc { get; }        
    }
}
