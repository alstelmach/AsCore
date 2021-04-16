using MediatR;

namespace ASCore.Application.Abstractions.Messaging.Commands
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }
}
