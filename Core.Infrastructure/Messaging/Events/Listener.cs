using System;
using Core.Infrastructure.Messaging.MessageBrokers.RabbitMQ;
using Core.Utilities.Extensions;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.Messaging.Events
{
    public abstract class Listener : BackgroundService
    {
        private readonly ILogger _logger;

        protected Listener(IBusControl busControl,
            IServiceProvider serviceProvider,
            IOptions<RabbitMQSettings> busConfiguration,
            ILogger logger)
        {
            _logger = logger;
            BusControl = busControl;
            ServiceProvider = serviceProvider;
            BusConfiguration = busConfiguration.Value;
        }

        protected IBusControl BusControl { get; }
        protected IServiceProvider ServiceProvider { get; }
        protected RabbitMQSettings BusConfiguration { get; }

        protected void HandleException(Exception exception)
        {
            var message = exception.GetDetailedMessage();
            _logger.LogError(exception, message);
        }
    }
}
