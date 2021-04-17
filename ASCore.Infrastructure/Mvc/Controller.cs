using System;
using AsCore.Application.Abstractions.Messaging.Commands;
using AsCore.Application.Abstractions.Messaging.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AsCore.Infrastructure.Mvc
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
