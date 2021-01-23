using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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

        public virtual void Create(params TEntity[] entity)
        {
            logger.LogTrace($"Creating {entity.Length} new {nameof(TEntity)}");
            Set.AddRange(entity);
        }

        public virtual void Update(params TEntity[] entity)
        {
            logger.LogTrace($"Updating {nameof(TEntity)} with Id {string.Join(", ", entity.Select(e => e.Id))}");

            // Get all entities that matches IDs
            var existingEntities = Set.Where(e => entity.Select(n => n.Id).Contains(e.Id));

            // If not all entities were found, log a debug message
            if (existingEntities.Count() != entity.Count())
            {
                var missingIds = entity.Select(e => e.Id).Where(id => !existingEntities.Select(ee => ee.Id).Contains(id));
                logger.LogDebug($"Couldn't find {nameof(TEntity)} with Id {string.Join(", ", missingIds)} for update");

                // TODO: Add a createIfNotExists option
            }

            foreach (var e in existingEntities)
            {
                e.Copy(entity.SingleOrDefault(n => n.Id.Equals(e.Id)));
            }
        }

        public virtual void Delete(params TId[] id)
        {
            logger.LogTrace($"Deleting {nameof(TEntity)} with Id {string.Join(", ", id)}");

            var entities = Set.Where(e => id.Contains(e.Id));

            if (entities.Count() != id.Length)
            {
                var missingIds = id.Where(i => !entities.Select(e => e.Id).Contains(i));
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
