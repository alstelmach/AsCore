using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence.BuildingBlocks
{
    public abstract class ReadModelRepository<TObject> where TObject : class
    {
        protected ReadModelRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }
        
        protected DbContext DbContext { get; }

        public virtual async Task CreateAsync(TObject @object)
        {
            await DbContext
                .Set<TObject>()
                .AddAsync(@object);

            await DbContext.SaveChangesAsync();
            DbContext.Entry(@object).State = EntityState.Detached;
        }

        public virtual async Task<IEnumerable<TObject>> QueryAsync(string sql,
            CancellationToken cancellationToken = default) =>
                await DbContext
                    .Database
                    .GetDbConnection()
                    .QueryAsync<TObject>(sql, cancellationToken);

        public virtual async Task<TObject> QueryFirstOrDefaultAsync(string sql,
            CancellationToken cancellationToken = default) =>
                await DbContext
                    .Database
                    .GetDbConnection()
                    .QueryFirstOrDefaultAsync<TObject>(sql, cancellationToken);

        public virtual async Task UpdateAsync(TObject @object)
        {
            DbContext
                .Set<TObject>()
                .Update(@object);

            await DbContext.SaveChangesAsync();
            DbContext.Entry(@object).State = EntityState.Detached;
        }

        public virtual async Task DeleteAsync(TObject @object)
        {
            DbContext
                .Set<TObject>()
                .Remove(@object);

            await DbContext.SaveChangesAsync();
        }
    }
}
