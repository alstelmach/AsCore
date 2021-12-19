using System;
using AsCore.Domain.Abstractions.Ports;

namespace AsCore.Infrastructure.Providers
{
    public class CommonDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime Now => DateTime.Now;
    }
}
