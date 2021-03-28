using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Infrastructure.Persistence.BuildingBlocks
{
    public interface IReadModelRepository<TObject> where TObject : class
    {
        Task CreateAsync(TObject @object);
        Task<IEnumerable<TObject>> QueryAsync(string sql);
        Task<TObject> QueryFirstAsync(string sql);
        Task UpdateAsync(TObject @object);
        Task DeleteAsync(TObject @object);
    }
}
