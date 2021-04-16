using System.Threading;
using System.Threading.Tasks;
using ASCore.Application.Abstractions.Messaging.Commands;
using MediatR;

namespace ASCore.Infrastructure.Messaging.Commands
{
    public sealed class CommandBus : Bus,
        ICommandBus
    {
        public CommandBus(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task SendAsync(ICommand command, CancellationToken cancellationToken) =>
            await Mediator
                .Send(command, cancellationToken);
    }
}
