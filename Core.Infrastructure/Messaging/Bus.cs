﻿using MediatR;

namespace Core.Infrastructure.Messaging
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
