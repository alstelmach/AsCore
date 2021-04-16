using System.Threading;
using System.Threading.Tasks;
using ASCore.Application.Abstractions.Messaging.Queries;
using MediatR;


namespace ASCore.Infrastructure.Messaging.Queries
{
    public sealed class QueryBus : Bus,
        IQueryBus
    {
        public QueryBus(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query,
            CancellationToken cancellationToken = default) =>
                await Mediator.Send(query,
                    cancellationToken);
    }
}
