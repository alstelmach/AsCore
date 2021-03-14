using System.Threading.Tasks;

namespace Core.Domain.Abstractions.Components
{
    public interface IDomainEventPublisher
    {
        Task PublishAsync(params IDomainEvent[] events);
    }
}
