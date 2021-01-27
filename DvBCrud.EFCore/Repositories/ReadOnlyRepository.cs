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
            return Set;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        public virtual TEntity Get(TId id)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            return Set.SingleOrDefault(e => id.Equals(e.Id));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        public virtual Task<TEntity> GetAsync(TId id)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            return Set.SingleOrDefaultAsync(e => id.Equals(e.Id));
        }
    }
}
