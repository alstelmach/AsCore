using System.Threading.Tasks;

namespace Core.Domain.Abstractions.BuildingBlocks
{
    public interface IRepository<in TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        Task CommitAsync(TAggregateRoot aggregateRoot);
    }
}
