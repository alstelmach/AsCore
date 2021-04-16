using System.Threading.Tasks;

namespace ASCore.Domain.Abstractions.BuildingBlocks
{
    public interface IDomainEventPublisher
    {
        Task PublishAsync(params IDomainEvent[] events);
    }
}
