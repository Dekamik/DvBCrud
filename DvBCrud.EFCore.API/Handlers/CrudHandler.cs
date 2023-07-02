using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using DvBCrud.EFCore.Repositories;

// ReSharper disable MemberCanBePrivate.Global

namespace DvBCrud.EFCore.API.Handlers;

[ExcludeFromCodeCoverage]  // Ignored because this is a simple proxy
public abstract class CrudHandler<TId, TRepository, TModel> : ICrudHandler<TId, TModel>
    where TRepository : IRepository<TId, TModel>
    where TModel : class
{
    protected readonly TRepository Repository;

    protected CrudHandler(TRepository repository)
    {
        Repository = repository;
    }

    public virtual IEnumerable<TModel> List()
    {
        return Repository.List();
    }

    public virtual TModel? Get(TId id)
    {
        return Repository.Get(id);
    }

    public virtual Task<TModel?> GetAsync(TId id)
    {
        return Repository.GetAsync(id);
    }

    public virtual TId Create(TModel model)
    {
        return Repository.Create(model);
    }

    public virtual async Task<TId> CreateAsync(TModel model)
    {
        return await Repository.CreateAsync(model);
    }

    public virtual void Update(TId id, TModel model)
    {
        Repository.Update(id, model);
    }
    
    public virtual async Task UpdateAsync(TId id, TModel model)
    {
        await Repository.UpdateAsync(id, model);
    }

    public virtual void Delete(TId id)
    {
        Repository.Delete(id);
    }
    
    public virtual async Task DeleteAsync(TId id)
    {
        await Repository.DeleteAsync(id);
    }
}