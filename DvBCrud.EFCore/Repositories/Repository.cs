﻿using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

            Set.Add(entity);
        }

        public virtual void Update(TId id, TEntity entity)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} cannot be null");

            var existingEntity = Set.Find(id);

            // If entity wasn't found, log a debug message
            if (existingEntity == null)
            {
                var message = $"{nameof(TEntity)} {id} not found";
                logger.LogDebug(message);
                throw new KeyNotFoundException(message);
            }

            existingEntity.Copy(entity);
        }

        public virtual async Task UpdateAsync(TId id, TEntity entity)
        {
            if (id == null)
                throw new ArgumentNullException($"{nameof(id)} cannot be null");

            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} cannot be null");

            var existingEntity = await Set.FindAsync(id);

            // If entity wasn't found, log a debug message
            if (existingEntity == null)
            {
                logger.LogDebug($"{nameof(TEntity)} {id} not found");
                return;
            }

            existingEntity.Copy(entity);
        }

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
