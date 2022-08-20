using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
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
        protected readonly TDbContext Context;

        private DbSet<TEntity> Set => Context.Set<TEntity>();

        public Repository(TDbContext context)
        {
            Context = context;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Set;
        }

        /// <inheritdoc/>
        public virtual TEntity? Get(TId id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return Set.FirstOrDefault(e => e.Id != null && e.Id.Equals(id));
        }

        /// <inheritdoc/>
        public virtual Task<TEntity?> GetAsync(TId id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return Set.FirstOrDefaultAsync(e => e.Id != null && e.Id.Equals(id));
        }

        /// <inheritdoc/>
        public virtual void Create(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Set.Add(entity);
        }

        /// <inheritdoc/>
        public virtual void Update(TId id, TEntity entity)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var existingEntity = Set.Find(id);

            if (existingEntity == null)
            {
                var message = $"{typeof(TEntity).Name} {id} not found";
                throw new KeyNotFoundException(message);
            }

            existingEntity.Copy(entity);
        }

        /// <inheritdoc/>
        public virtual async Task UpdateAsync(TId id, TEntity entity)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var existingEntity = await Set.FindAsync(id);

            if (existingEntity == null)
            {
                var message = $"{typeof(TEntity).Name} {id} not found";
                throw new KeyNotFoundException(message);
            }

            existingEntity.Copy(entity);
        }

        /// <inheritdoc/>
        public virtual void Delete(TId id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var entity = Set.Find(id);

            if (entity == null)
            {
                var message = $"{typeof(TEntity).Name} {id} not found";
                throw new KeyNotFoundException(message);
            }

            Set.Remove(entity);
        }

        /// <inheritdoc/>
        public virtual async Task DeleteAsync(TId id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var entity = await Set.FindAsync(id);

            if (entity == null)
            {
                var message = $"Couldn't find {typeof(TEntity).Name} with Id {id} for deletion";
                throw new KeyNotFoundException(message);
            }

            Set.Remove(entity);
        }

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        public virtual Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
