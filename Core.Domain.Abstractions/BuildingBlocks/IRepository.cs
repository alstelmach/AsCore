using System.Threading.Tasks;

namespace Core.Domain.Abstractions.BuildingBlocks
{
    public interface IRepository
    {
        Task CommitAsync();
    }
}
