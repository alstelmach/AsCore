namespace AsCore.Infrastructure.Messaging.MessageBrokers.RabbitMQ
{
    public sealed record RabbitMQSettings(
        string HostName,
        string VirtualHostName,
        string UserName,
        string Password);
}
