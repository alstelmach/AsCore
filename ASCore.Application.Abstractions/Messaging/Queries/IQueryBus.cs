using System.Threading;
using System.Threading.Tasks;

namespace ASCore.Application.Abstractions.Messaging.Queries
{
    public interface IQueryBus
    {
        Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
    }
}
