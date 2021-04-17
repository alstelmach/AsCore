using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsCore.Application.Abstractions.Repositories
{
    public interface IReadModelRepository<TObject> where TObject : class
    {
        Task CreateAsync(TObject @object);
        Task<IEnumerable<TObject>> QueryAsync(string sql, CancellationToken cancellationToken = default);
        Task<TObject> QueryFirstOrDefaultAsync(string sql, CancellationToken cancellationToken = default);
        Task UpdateAsync(TObject @object);
        Task DeleteAsync(TObject @object);
    }
}
