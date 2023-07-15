using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DvBCrud.Shared;
using DvBCrud.Shared.Entities;
using DvBCrud.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

// ReSharper disable MemberCanBePrivate.Global

namespace DvBCrud.EFCore;

public abstract class Repository<TEntity, TId, TDbContext, TMapper, TModel, TFilter> : IRepository<TId, TModel, TFilter>
    where TEntity : class, IEntity<TId>
    where TDbContext : DbContext 
    where TMapper : IMapper<TEntity, TModel, TFilter>
{
    protected readonly TDbContext Context;
    protected readonly TMapper Mapper;

    protected DbSet<TEntity> Set => Context.Set<TEntity>();

    protected IQueryable<TEntity> QueryableWithIncludes { get; init; }

    protected Repository(TDbContext context, TMapper mapper)
    {
        Context = context;
        Mapper = mapper;
        QueryableWithIncludes = Set.AsQueryable();
    }

    /// <inheritdoc/>
    public virtual IEnumerable<TModel> List(TFilter filter)
    {
        return Mapper.FilterOrderAndPaginate(QueryableWithIncludes, filter)
            .Select(Mapper.ToModel);
    }

    /// <inheritdoc/>
    public virtual TModel Get(TId id)
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var entity = QueryableWithIncludes.FirstOrDefault(e => e.Id != null && e.Id.Equals(id));
        if (entity == null)
        {
            throw new NotFoundException($"{typeof(TEntity).Name} {id} not found");
        }

        return Mapper.ToModel(entity);
    }

    /// <inheritdoc/>
    public virtual async Task<TModel> GetAsync(TId id)
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var entity = await QueryableWithIncludes.FirstOrDefaultAsync(e => e.Id != null && e.Id.Equals(id));
        if (entity == null)
        {
            throw new NotFoundException($"{typeof(TEntity).Name} {id} not found");
        }

        return Mapper.ToModel(entity);
    }

    /// <inheritdoc/>
    public virtual TId Create(TModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var entity = Mapper.ToEntity(model);
        Set.Add(entity);
        Context.SaveChanges();
        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<TId> CreateAsync(TModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var entity = Mapper.ToEntity(model);
        Set.Add(entity);
        await Context.SaveChangesAsync();
        return entity.Id;
    }

    /// <inheritdoc/>
    public virtual void Update(TId id, TModel model)
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var existingEntity = Set.Find(id);
        if (existingEntity == null)
        {
            throw new NotFoundException($"{typeof(TEntity).Name} {id} not found");
        }

        var entity = Mapper.ToEntity(model);
        Mapper.UpdateEntity(existingEntity, entity);
        Context.SaveChanges();
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync(TId id, TModel model)
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var existingEntity = await Set.FindAsync(id);
        if (existingEntity == null)
        {
            throw new NotFoundException($"{typeof(TEntity).Name} {id} not found");
        }

        var entity = Mapper.ToEntity(model);
        Mapper.UpdateEntity(existingEntity, entity);
        await Context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public virtual void Delete(TId id)
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var entity = Set.Find(id);

        if (entity == null)
        {
            throw new NotFoundException();
        }

        Set.Remove(entity);
        Context.SaveChanges();
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(TId id)
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var entity = await Set.FindAsync(id);

        if (entity == null)
        {
            throw new NotFoundException($"{typeof(TEntity).Name} {id} not found");
        }

        Set.Remove(entity);
        await Context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public virtual bool Exists(TId id)
    {
        return Set.Any(entity => entity.Id!.Equals(id));
    }
}