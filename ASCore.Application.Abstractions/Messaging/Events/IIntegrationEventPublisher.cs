using System.Threading.Tasks;

namespace AsCore.Application.Abstractions.Messaging.Events
{
    public interface IIntegrationEventPublisher
    {
        Task PublishAsync(params IntegrationEvent[] events);
    }
}
