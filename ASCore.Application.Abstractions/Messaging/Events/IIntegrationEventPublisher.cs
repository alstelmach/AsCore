using System.Threading.Tasks;

namespace ASCore.Application.Abstractions.Messaging.Events
{
    public interface IIntegrationEventPublisher
    {
        Task PublishAsync(params IntegrationEvent[] events);
    }
}
