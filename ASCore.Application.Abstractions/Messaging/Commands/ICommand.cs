using MediatR;

namespace ASCore.Application.Abstractions.Messaging.Commands
{
    public interface ICommand : IIdentityProvider,
        IRequest
    {
    }
}
