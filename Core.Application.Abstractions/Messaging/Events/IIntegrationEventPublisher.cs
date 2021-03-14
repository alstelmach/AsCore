using System.Threading.Tasks;

namespace Core.Application.Abstractions.Messaging.Events
{
    public interface IIntegrationEventPublisher
    {
        Task PublishAsync(params IntegrationEvent[] events);
    }
}
