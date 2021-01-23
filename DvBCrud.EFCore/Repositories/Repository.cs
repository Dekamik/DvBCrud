using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
            logger.LogTrace($"Creating a new {nameof(TEntity)}");
            Set.Add(entity);
        }

        public virtual void CreateRange(IEnumerable<TEntity> entities)
        {
            logger.LogTrace($"Creating {entities.Count()} new {nameof(TEntity)}");
            Set.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            logger.LogTrace($"Updating {nameof(TEntity)} with Id {entity.Id}");

            var existingEntity = Set.SingleOrDefault(e => e.Id.Equals(entity.Id));

            // If entity wasn't found, log a debug message
            if (existingEntity == null)
            {
                logger.LogDebug($"Couldn't find {nameof(TEntity)} with Id {entity.Id} for update");

                // TODO: Add a createIfNotExists option
            }

            existingEntity.Copy(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            logger.LogTrace($"Updating {entities.Count()} {nameof(TEntity)} with Id {string.Join(", ", entities.Select(e => e.Id))}");

            // Get all entities that matches IDs
            var existingEntities = Set.Where(e => entities.Select(n => n.Id).Contains(e.Id));

            // If not all entities were found, log a debug message
            if (existingEntities.Count() != entities.Count())
            {
                var missingIds = entities.Select(e => e.Id).Where(id => !existingEntities.Select(ee => ee.Id).Contains(id));
                logger.LogDebug($"Couldn't find {nameof(TEntity)} with Id {string.Join(", ", missingIds)} for update");

                // TODO: Add a createIfNotExists option
            }

            foreach (var e in existingEntities)
            {
                e.Copy(entities.SingleOrDefault(n => n.Id.Equals(e.Id)));
            }
        }

        public virtual void Delete(TId id)
        {
            logger.LogTrace($"Deleting {nameof(TEntity)} with Id {string.Join(", ", id)}");

            var entity = Set.Where(e => e.Id.Equals(id));

            if (entity == null)
            {
                logger.LogDebug($"Couldn't find {nameof(TEntity)} with Id {id} for deletion");
                return;
            }

            Set.RemoveRange(entity);
        }

        public virtual void DeleteRange(IEnumerable<TId> ids)
        {
            logger.LogTrace($"Deleting {ids.Count()} {nameof(TEntity)} with Id {string.Join(", ", ids)}");

            var entities = Set.Where(e => ids.Contains(e.Id));

            if (entities.Count() != ids.Count())
            {
                var missingIds = ids.Where(i => !entities.Select(e => e.Id).Contains(i));
                logger.LogDebug($"Couldn't find {nameof(TEntity)} with Id {string.Join(", ", missingIds)} for deletion");
            }

            Set.RemoveRange(entities);
        }

        public virtual async Task SaveChanges()
        {
            logger.LogTrace($"Repository for {nameof(TEntity)} saving changes to {nameof(TDbContext)}");
            await dbContext.SaveChangesAsync();
        }
    }
}
