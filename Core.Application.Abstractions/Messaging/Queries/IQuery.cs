using MediatR;

namespace Core.Application.Abstractions.Messaging.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>,
        IIdentityProvider
    {
    }
}
