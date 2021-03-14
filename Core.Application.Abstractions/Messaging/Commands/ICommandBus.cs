using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Abstractions.Messaging.Commands
{
    public interface ICommandBus
    {
        Task SendAsync(ICommand command, CancellationToken cancellationToken = default);
    }
}
