namespace AsCore.Domain.Abstractions.BuildingBlocks
{
    public abstract class Entity<TKey> : IIdentifiable<TKey>
    {
        protected Entity(TKey id)
        {
            Id = id;
        }

        public TKey Id { get; protected set; }
    }
}
