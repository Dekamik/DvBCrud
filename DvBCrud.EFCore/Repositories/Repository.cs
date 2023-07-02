using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DvBCrud.EFCore.Mapping;

// ReSharper disable MemberCanBePrivate.Global

namespace DvBCrud.EFCore.Repositories
{
    public abstract class Repository<TEntity, TId, TDbContext, TMapper, TModel> : IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TDbContext : DbContext 
        where TMapper : IMapper<TEntity, TModel>
    {
        protected readonly TDbContext Context;
        protected readonly TMapper Mapper;

        protected DbSet<TEntity> Set => Context.Set<TEntity>();

        protected IQueryable<TEntity> QueryableWithIncludes { get; init; }

        public Repository(TDbContext context, TMapper mapper)
        {
            Context = context;
            Mapper = mapper;
            QueryableWithIncludes = Set.AsQueryable();
        }

        public virtual IQueryable<TEntity> List()
        {
            return QueryableWithIncludes;
        }

        /// <inheritdoc/>
        public virtual TEntity? Get(TId id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return QueryableWithIncludes.FirstOrDefault(e => e.Id != null && e.Id.Equals(id));
        }

        /// <inheritdoc/>
        public virtual Task<TEntity?> GetAsync(TId id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return QueryableWithIncludes.FirstOrDefaultAsync(e => e.Id != null && e.Id.Equals(id));
        }

        /// <inheritdoc/>
        public virtual void Create(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Set.Add(entity);
            Context.SaveChanges();
        }

        public async Task CreateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Set.Add(entity);
            await Context.SaveChangesAsync();
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

            Mapper.UpdateEntity(entity, existingEntity);
            Context.SaveChanges();
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
            
            Mapper.UpdateEntity(entity, existingEntity);
            await Context.SaveChangesAsync();
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
            Context.SaveChanges();
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
            await Context.SaveChangesAsync();
        }

        public virtual bool Exists(TId id) => Set.Any(entity => entity.Id!.Equals(id));
    }
}
