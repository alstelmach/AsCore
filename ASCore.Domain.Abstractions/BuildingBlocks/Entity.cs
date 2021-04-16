using System;

namespace ASCore.Domain.Abstractions.BuildingBlocks
{
    public abstract class Entity : IIdentifiable
    {
        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
