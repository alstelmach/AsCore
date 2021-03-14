using System.Threading.Tasks;

namespace Core.Domain.Abstractions.BuildingBlocks
{
    public interface IDomainEventPublisher
    {
        Task PublishAsync(params IDomainEvent[] events);
    }
}
