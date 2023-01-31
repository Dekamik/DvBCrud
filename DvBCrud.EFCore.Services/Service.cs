using DvBCrud.Common.Services.Mapping;
using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;

namespace DvBCrud.EFCore.Services;

public abstract class Service<TEntity, TId, TRepository, TModel, TMapper> : IService<TId, TModel> 
    where TEntity : BaseEntity<TId>
    where TRepository : IRepository<TEntity, TId>
    where TModel : class
    where TMapper : IMapper<TEntity, TModel>
{
    protected readonly TRepository Repository;
    protected readonly TMapper Mapper;

    public Service(TRepository repository, TMapper mapper)
    {
        Mapper = mapper;
        Repository = repository;
    }

    public virtual IEnumerable<TModel> GetAll() => Repository.GetAll().Select(Mapper.ToModel);

    public virtual TModel? Get(TId id)
    {
        var entity = Repository.Get(id);
        return entity == null ? null : Mapper.ToModel(entity);
    }

    public virtual async Task<TModel?> GetAsync(TId id)
    {
        var entity = await Repository.GetAsync(id);
        return entity == null ? null : Mapper.ToModel(entity);
    }

    public virtual TId Create(TModel model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        var entity = Mapper.ToEntity(model);
        Repository.Create(entity);
        Repository.SaveChanges();
        return entity.Id;
    }

    public virtual async Task<TId> CreateAsync(TModel model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        var entity = Mapper.ToEntity(model);
        Repository.Create(entity);
        await Repository.SaveChangesAsync();
        return entity.Id;
    }

    public virtual void Update(TId id, TModel model)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        
        Repository.Update(id, Mapper.ToEntity(model));
        Repository.SaveChanges();
    }
    
    public virtual async Task UpdateAsync(TId id, TModel model)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        
        await Repository.UpdateAsync(id, Mapper.ToEntity(model));
        await Repository.SaveChangesAsync();
    }

    public virtual void Delete(TId id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        
        Repository.Delete(id);
        Repository.SaveChanges();
    }
    
    public virtual async Task DeleteAsync(TId id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        
        await Repository.DeleteAsync(id);
        await Repository.SaveChangesAsync();
    }
}