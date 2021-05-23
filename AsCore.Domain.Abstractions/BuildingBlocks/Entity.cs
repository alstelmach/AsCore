using System;

namespace AsCore.Domain.Abstractions.BuildingBlocks
{
    public abstract class Entity : IIdentifiable
    {
        protected Entity(Guid id)
        {
            Id = id;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public Guid Id { get; protected set; }
    }
}
