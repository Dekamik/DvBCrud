using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.Repositories
{
    public abstract class Repository<TEntity, TId, TDbContext> : IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TDbContext : DbContext
    {
        internal readonly TDbContext dbContext;

        internal readonly ILogger logger;

        private DbSet<TEntity> Set => dbContext.Set<TEntity>();

        public Repository(TDbContext dbContext, ILogger logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Set.AsNoTracking();
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        public virtual TEntity Get(TId id)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            return Set.AsNoTracking().FirstOrDefault(e => e.Id.Equals(id));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        public virtual Task<TEntity> GetAsync(TId id)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            return Set.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null</exception>
        public virtual void Create(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} cannot be null");

            Set.Add(entity);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null</exception>
        /// <exception cref="KeyNotFoundException">Thrown if <typeparamref name="TEntity"/> with <paramref name="id"/> not found</exception>"
        public virtual void Update(TId id, TEntity entity)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} cannot be null");

            var existingEntity = Set.Find(id);

            if (existingEntity == null)
            {
                var message = $"{nameof(TEntity)} {id} not found";
                logger.LogDebug(message);
                throw new KeyNotFoundException(message);
            }

            existingEntity.Copy(entity);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null</exception>
        /// <exception cref="KeyNotFoundException">Thrown if <typeparamref name="TEntity"/> with <paramref name="id"/> not found</exception>"
        public virtual async Task UpdateAsync(TId id, TEntity entity)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} cannot be null");

            var existingEntity = await Set.FindAsync(id);

            if (existingEntity == null)
            {
                var message = $"{nameof(TEntity)} {id} not found";
                logger.LogDebug(message);
                throw new KeyNotFoundException(message);
            }

            existingEntity.Copy(entity);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        /// <exception cref="KeyNotFoundException">Thrown if <typeparamref name="TEntity"/> with <paramref name="id"/> not found</exception>"
        public virtual void Delete(TId id)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            var entity = Set.Find(id);

            if (entity == null)
            {
                var message = $"{nameof(TEntity)} {id} not found";
                logger.LogDebug(message);
                throw new KeyNotFoundException(message);
            }

            Set.Remove(entity);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        /// <exception cref="KeyNotFoundException">Thrown if <typeparamref name="TEntity"/> with <paramref name="id"/> not found</exception>"
        public virtual async Task DeleteAsync(TId id)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            var entity = await Set.FindAsync(id);

            if (entity == null)
            {
                var message = $"Couldn't find {nameof(TEntity)} with Id {id} for deletion";
                logger.LogDebug(message);
                throw new KeyNotFoundException(message);
            }

            Set.Remove(entity);
        }

        public virtual void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public virtual Task SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
