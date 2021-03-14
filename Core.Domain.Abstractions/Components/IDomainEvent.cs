using System;
using MediatR;

namespace Core.Domain.Abstractions.Components
{
    public interface IDomainEvent : INotification,
        IIdentifiable
    {
        DateTime CreatedAtUtc { get; }        
    }
}
