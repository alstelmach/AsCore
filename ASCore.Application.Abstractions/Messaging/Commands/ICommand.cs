using MediatR;

namespace AsCore.Application.Abstractions.Messaging.Commands
{
    public interface ICommand : IIdentityProvider,
        IRequest
    {
    }
}
