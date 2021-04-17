using System.Threading.Tasks;

namespace AsCore.Domain.Abstractions.BuildingBlocks
{
    public interface IDomainEventPublisher
    {
        Task PublishAsync(params IDomainEvent[] events);
    }
}
