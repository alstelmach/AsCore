using System.Collections.Generic;
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
        }

        public virtual async Task<IEnumerable<TObject>> QueryAsync(string sql) =>
            await DbContext
                .Database
                .GetDbConnection()
                .QueryAsync<TObject>(sql);

        public virtual async Task<TObject> QueryFirstAsync(string sql) =>
            await DbContext
                .Database
                .GetDbConnection()
                .QueryFirstAsync<TObject>(sql);

        public virtual async Task UpdateAsync(TObject @object)
        {
            DbContext
                .Set<TObject>()
                .Update(@object);

            await DbContext.SaveChangesAsync();
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
