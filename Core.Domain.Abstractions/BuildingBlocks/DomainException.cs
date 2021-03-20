using System;

namespace Core.Domain.Abstractions.BuildingBlocks
{
    [Serializable]
    public abstract class DomainException : Exception
    {
        protected DomainException(string message)
            : base(message)
        {
        }

        protected DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
