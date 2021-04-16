using MediatR;

namespace ASCore.Application.Abstractions.Messaging.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>,
        IIdentityProvider
    {
    }
}
