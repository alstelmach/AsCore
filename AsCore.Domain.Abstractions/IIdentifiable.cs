using System;

namespace AsCore.Domain.Abstractions
{
    public interface IIdentifiable
    {
        Guid Id { get; }        
    }
}
