using System;
using ASCore.Application.Abstractions.Messaging.Commands;
using ASCore.Application.Abstractions.Messaging.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASCore.Infrastructure.Mvc
{
    public abstract class Controller : ControllerBase
    {
        protected Controller(ICommandBus commandBus,
            IQueryBus queryBus)
        {
            CommandBus = commandBus;
            QueryBus = queryBus;
        }
        
        protected const string RoutePattern = "api/v{version:apiVersion}/[controller]";
        protected const string DefaultApiVersion = "1.0";
        
        protected ICommandBus CommandBus { get; }
        protected IQueryBus QueryBus { get; }

        protected static string GetUri(Guid resourceId,
            string resourceName,
            string version = DefaultApiVersion) =>
                $"api/v{version}/{resourceName}/{resourceId}";
    }
}
