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
            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} cannot be null");

            logger.LogTrace($"Creating a new {nameof(TEntity)}");
            Set.Add(entity);
        }

        public virtual void CreateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException($"{nameof(entities)} cannot be null");

            logger.LogTrace($"Creating {entities.Count()} new {nameof(TEntity)}");
            Set.AddRange(entities);
        }

        public virtual void Update(TEntity entity, bool createIfNotExists = false)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} cannot be null");

            logger.LogTrace($"Updating {nameof(TEntity)} with Id {entity.Id}");

            var existingEntity = Set.Find(entity.Id);

            // If entity wasn't found, log a debug message
            if (existingEntity == null)
            {
                logger.LogDebug($"Couldn't find {nameof(TEntity)} with Id {entity.Id} for update{ (createIfNotExists ? ", creating entity" : "") }");

                if (createIfNotExists)
                {
                    Create(entity);
                }

                return;
            }

            existingEntity.Copy(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities, bool createIfNotExists = false)
        {
            if (entities == null)
                throw new ArgumentNullException($"{nameof(entities)} cannot be null");

            logger.LogTrace($"Updating {entities.Count()} {nameof(TEntity)} with Id {string.Join(", ", entities.Select(e => e.Id))}");

            // Get all entities that matches IDs
            var existingEntities = Set.Where(e => entities.Select(n => n.Id).Contains(e.Id));

            // If not all entities were found, log a debug message
            if (existingEntities.Count() != entities.Count())
            {
                var missingIds = entities.Select(e => e.Id).Where(id => !existingEntities.Select(ee => ee.Id).Contains(id));
                logger.LogDebug($"Couldn't find {entities.Count()} {nameof(TEntity)} with Id {string.Join(", ", missingIds)} for update{(createIfNotExists ? ", creating those entities" : "")}");

                if (createIfNotExists)
                {
                    var missingEntities = entities.Where(e => missingIds.Contains(e.Id));
                    CreateRange(missingEntities);
                }
            }

            foreach (var e in existingEntities)
            {
                e.Copy(entities.SingleOrDefault(n => n.Id.Equals(e.Id)));
            }
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

        public virtual void DeleteRange(IEnumerable<TId> ids)
        {
            if (ids == null)
                throw new ArgumentNullException($"{nameof(ids)} cannot be null");

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
