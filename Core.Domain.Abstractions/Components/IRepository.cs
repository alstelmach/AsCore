using System.Threading.Tasks;

namespace Core.Domain.Abstractions.Components
{
    public interface IRepository
    {
        Task CommitAsync();
    }
}
