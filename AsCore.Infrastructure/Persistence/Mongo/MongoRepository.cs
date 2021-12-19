using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AsCore.Domain.Abstractions.BuildingBlocks;
using AsCore.Utilities.Extensions;
using MongoDB.Driver;

namespace AsCore.Infrastructure.Persistence.Mongo
{
    public class MongoRepository<TDocument, TKey> : IRepository<TDocument, TKey>,
        IDisposable where TDocument : IIdentifiable<TKey>
    {
        private readonly IDomainEventPublisher _domainEventPublisher;
        private readonly ICollection<TDocument> _trackedDocuments = new List<TDocument>();
        
        public MongoRepository(
            IMongoClient mongoClient,
            MongoRepositorySettings<TDocument> settings,
            IDomainEventPublisher domainEventPublisher = default)
        {
            _domainEventPublisher = domainEventPublisher;
        
            Session = mongoClient.StartSession();
            Collection = mongoClient
                .GetDatabase(settings.DatabaseName)
                .GetCollection<TDocument>(settings.CollectionName);
        }
        
        protected IClientSessionHandle Session { get; }
        protected IMongoCollection<TDocument> Collection { get; }
        
        public virtual async Task CreateAsync(
            TDocument document,
            CancellationToken cancellationToken = default)
        {
            EnsureTransactionHasStarted();
        
            var options = new InsertOneOptions();
        
            await Collection.InsertOneAsync(
                Session,
                document,
                options,
                cancellationToken);
            
            EnsureDocumentIsTracked(document);
        }
        
        public virtual async Task CreateManyAsync(
            ICollection<TDocument> documents,
            CancellationToken cancellationToken = default)
        {
            EnsureTransactionHasStarted();
        
            var options = new InsertManyOptions();
        
            await Collection.InsertManyAsync(
                Session,
                documents,
                options,
                cancellationToken);
            
            EnsureDocumentsAreTracked(documents);
        }
        
        public virtual async Task<ICollection<TDocument>> GetAsync(
            Expression<Func<TDocument, bool>> filterExpression,
            CancellationToken cancellationToken = default)
        {
            var cursor = await Collection.FindAsync(
                Session,
                filterExpression,
                cancellationToken: cancellationToken);
        
            var results = await cursor.ToListAsync(cancellationToken);
        
            return results;
        }
        
        public virtual async Task<ICollection<TDocument>> GetAsync(
            CancellationToken cancellationToken = default) =>
                await GetAsync(
                    _ => true,
                    cancellationToken);
        
        public virtual async Task<TDocument> GetAsync(
            TKey key,
            CancellationToken cancellationToken = default)
        {
            var filterDefinition = Builders<TDocument>
                .Filter
                .Eq(document => document.Id, key);
        
            var cursor = await Collection.FindAsync(
                Session,
                filterDefinition,
                cancellationToken: cancellationToken);
        
            var result = await cursor.FirstOrDefaultAsync(cancellationToken);
        
            return result;
        }
        
        public virtual async Task UpdateAsync(
            TDocument updatedDocument,
            CancellationToken cancellationToken = default)
        {
            EnsureTransactionHasStarted();
            
            var filterDefinition = Builders<TDocument>
                .Filter
                .Eq(document => document.Id, updatedDocument.Id);
        
            await Collection.FindOneAndReplaceAsync(
                Session,
                filterDefinition,
                updatedDocument,
                cancellationToken: cancellationToken);
            
            EnsureDocumentIsTracked(updatedDocument);
        }
        
        public virtual async Task DeleteAsync(
            TKey key,
            CancellationToken cancellationToken = default)
        {
            EnsureTransactionHasStarted();
            
            var filterDefinition = Builders<TDocument>
                .Filter
                .Eq(document => document.Id, key);
        
            await Collection.FindOneAndDeleteAsync(
                Session,
                filterDefinition,
                cancellationToken: cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (!Session.IsInTransaction)
            {
                return;
            }
        
            await Session.CommitTransactionAsync(cancellationToken);

            var aggregateRoots = _trackedDocuments
                .Select(document => document as AggregateRoot<TKey>)
                .Where(document => document is not null)
                .ToList();

            var canOmitEventPublication = !aggregateRoots.Any();

            if (canOmitEventPublication)
            {
                return;
            }
            
            var events = aggregateRoots
                .Select(root => root.DequeueDomainEvents())
                .SelectMany(domainEvent => domainEvent)
                .ToArray();
        
            var isPropagationRequired = _domainEventPublisher is not null;
            _trackedDocuments.Clear();
        
            if (isPropagationRequired)
            {
                await _domainEventPublisher.PublishAsync(events);
            }
        }
        
        public virtual void Dispose()
        {
            Session.Dispose();
            GC.Collect();
        }
        
        private void EnsureTransactionHasStarted()
        {
            if (!Session.IsInTransaction)
            {
                Session.StartTransaction();
            }
        }
        
        private void EnsureDocumentIsTracked(TDocument document)
        {
            var doesDemandAddition = _trackedDocuments.All(root =>
                !root.Id.Equals(document.Id));
        
            if (doesDemandAddition)
            {
                _trackedDocuments.Add(document);
            }
        }
        
        private void EnsureDocumentsAreTracked(IEnumerable<TDocument> documents) =>
            documents
                .ForEach(EnsureDocumentIsTracked);
    }
}