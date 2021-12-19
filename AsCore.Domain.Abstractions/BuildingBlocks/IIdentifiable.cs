namespace AsCore.Domain.Abstractions.BuildingBlocks
{
    public interface IIdentifiable<out TKey>
    {
        TKey Id { get; }
    }
}
