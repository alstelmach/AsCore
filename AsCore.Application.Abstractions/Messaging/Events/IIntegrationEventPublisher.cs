using System.Threading.Tasks;

namespace AsCore.Application.Abstractions.Messaging.Events
{
    public interface IIntegrationEventPublisher
    {
        Task PublishAsync<TIntegrationEvent>(TIntegrationEvent @event)
            where TIntegrationEvent : IntegrationEvent;
    }
}
