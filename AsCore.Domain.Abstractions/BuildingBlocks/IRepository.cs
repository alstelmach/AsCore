namespace AsCore.Domain.Abstractions.BuildingBlocks
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
    }
}
