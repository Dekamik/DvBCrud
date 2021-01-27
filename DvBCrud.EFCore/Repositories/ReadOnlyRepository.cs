using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public virtual IEnumerable<TEntity> GetAll()
        {
            logger.LogTrace($"Getting all {nameof(TEntity)} entities");
            return Set;
        }

        public virtual Task<TEntity> Get(TId id)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            logger.LogTrace($"Getting {nameof(TEntity)} entity with Id {id}");
            return Set.SingleOrDefaultAsync(e => id.Equals(e.Id));
        }

        public virtual IEnumerable<TEntity> GetRange(IEnumerable<TId> ids)
        {
            if (ids == null)
                throw new ArgumentNullException($"{ids} cannot be null");

            logger.LogTrace($"Getting {nameof(TEntity)} entity with Id {string.Join(", ", ids)}");
            return Set.Where(e => ids.Contains(e.Id));
        }
    }
}
