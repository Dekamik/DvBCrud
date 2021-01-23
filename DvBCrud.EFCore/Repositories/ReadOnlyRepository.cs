using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DvBCrud.EFCore.Repositories
{
    public abstract class ReadOnlyRepository<TEntity, TId, TDbContext> : IReadOnlyRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TDbContext : DbContext
    {
        internal readonly TDbContext dbContext;

        internal readonly ILogger logger;

        private IQueryable<TEntity> Set => dbContext.Set<TEntity>().AsNoTracking();

        public ReadOnlyRepository(TDbContext dbContext, ILogger logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            logger.LogTrace($"Getting all {nameof(TEntity)} entities");
            return Set;
        }

        public virtual IQueryable<TEntity> Get(params TId[] id)
        {
            logger.LogTrace($"Getting {nameof(TEntity)} entity with Id {string.Join(", ", id)}");
            return GetAll().Where(e => id.Contains(e.Id));
        }
    }
}
