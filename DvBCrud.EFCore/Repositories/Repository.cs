using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.Repositories
{
    public abstract class Repository<TEntity, TId, TDbContext> : ReadOnlyRepository<TEntity, TId, TDbContext>, IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TDbContext : DbContext
    {
        private DbSet<TEntity> Set => dbContext.Set<TEntity>();

        public Repository(TDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {

        }

        public virtual void Create(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} cannot be null");

            logger.LogTrace($"Creating a new {nameof(TEntity)}");
            Set.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} cannot be null");

            logger.LogTrace($"Updating {nameof(TEntity)} with Id {entity.Id}");

            var existingEntity = Set.Find(entity.Id);

            // If entity wasn't found, log a debug message
            if (existingEntity == null)
            {
                logger.LogDebug($"{nameof(TEntity)} {entity.Id} not found");
                return;
            }

            existingEntity.Copy(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} cannot be null");

            logger.LogTrace($"Updating {nameof(TEntity)} with Id {entity.Id}");

            var existingEntity = await Set.FindAsync(entity.Id);

            // If entity wasn't found, log a debug message
            if (existingEntity == null)
            {
                logger.LogDebug($"{nameof(TEntity)} {entity.Id} not found");
                return;
            }

            existingEntity.Copy(entity);
        }

        public virtual void Delete(TId id)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            logger.LogTrace($"Deleting {nameof(TEntity)} with Id {string.Join(", ", id)}");

            var entity = Set.Find(id);

            if (entity == null)
            {
                logger.LogDebug($"Couldn't find {nameof(TEntity)} with Id {id} for deletion");
                return;
            }

            Set.RemoveRange(entity);
        }

        public virtual void SaveChanges()
        {
            logger.LogTrace($"Repository for {nameof(TEntity)} saving changes to {nameof(TDbContext)}");
            dbContext.SaveChanges();
        }

        public virtual Task SaveChangesAsync()
        {
            logger.LogTrace($"Repository for {nameof(TEntity)} saving changes to {nameof(TDbContext)}");
            return dbContext.SaveChangesAsync();
        }
    }
}
