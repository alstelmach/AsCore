using MediatR;

namespace AsCore.Application.Abstractions.Messaging.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
