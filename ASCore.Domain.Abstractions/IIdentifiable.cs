using System;

namespace ASCore.Domain.Abstractions
{
    public interface IIdentifiable
    {
        Guid Id { get; }        
    }
}
