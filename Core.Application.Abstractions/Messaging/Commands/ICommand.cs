using MediatR;

namespace Core.Application.Abstractions.Messaging.Commands
{
    public interface ICommand : IContract,
        IRequest
    {
    }
}
