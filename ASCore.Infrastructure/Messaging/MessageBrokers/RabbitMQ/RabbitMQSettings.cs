namespace ASCore.Infrastructure.Messaging.MessageBrokers.RabbitMQ
{
    public sealed class RabbitMQSettings
    {
        public string HostName { get; init; }
        public string VirtualHostName { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
