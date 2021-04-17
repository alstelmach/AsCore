using System.Threading.Tasks;

namespace AsCore.Application.Abstractions.Messaging.Events
{
    public interface IIntegrationEventHandler<in TIntegrationEvent>
        where TIntegrationEvent : IntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent @event);
    }
}
