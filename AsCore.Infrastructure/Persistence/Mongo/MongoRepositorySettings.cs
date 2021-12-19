namespace AsCore.Infrastructure.Persistence.Mongo
{
    public sealed class MongoRepositorySettings<TDocument>
    {
        public string DatabaseName { get; init; }
        public string CollectionName { get; init; }
    }
}
