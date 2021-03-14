using System.Threading.Tasks;

namespace Core.Application.Abstractions.Messaging.Events
{
    public interface IIntegrationEventHandler<in TIntegrationEvent>
        where TIntegrationEvent : IntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent @event);
    }
}
