namespace ASCore.Infrastructure.Persistence.Marten
{
    public sealed class EventStoreSettings
    {
        public string DatabaseOwner { get; init; }
        public string Encoding { get; init; }
        public int ConnectionLimit { get; init; }
    }
}
