using System;

namespace AsCore.Domain.Abstractions.Ports
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
        DateTime Now { get; }
    }
}
