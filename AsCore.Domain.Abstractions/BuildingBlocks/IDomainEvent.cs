using System;
using MediatR;

namespace AsCore.Domain.Abstractions.BuildingBlocks
{
    public interface IDomainEvent : INotification,
        IIdentifiable
    {
        DateTime CreatedAtUtc { get; }        
    }
}
