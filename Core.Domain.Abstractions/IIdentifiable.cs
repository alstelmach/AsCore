using System;

namespace Core.Domain.Abstractions
{
    public interface IIdentifiable
    {
        Guid Id { get; }        
    }
}
