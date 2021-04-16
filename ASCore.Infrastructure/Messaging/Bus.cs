using MediatR;

namespace ASCore.Infrastructure.Messaging
{
    public abstract class Bus
    {
        protected Bus(IMediator mediator)
        {
            Mediator = mediator;
        }
        
        protected IMediator Mediator { get; }
    }
}
