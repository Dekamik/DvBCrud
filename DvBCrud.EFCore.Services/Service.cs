using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;
using DvBCrud.EFCore.Services.Models;

namespace DvBCrud.EFCore.Services;

public abstract class Service<TEntity, TId, TRepository, TModel, TConverter> : IService<TId, TModel> 
    where TEntity : BaseEntity<TId>
    where TRepository : IRepository<TEntity, TId>
    where TModel : BaseModel
    where TConverter : IConverter<TEntity, TModel>
{
    protected readonly TRepository Repository;
    protected readonly TConverter Converter;

    public Service(TRepository repository, TConverter converter)
    {
        Converter = converter;
        Repository = repository;
    }

    public IEnumerable<TModel> GetAll() => Repository.GetAll().Select(Converter.ToModel);

    public TModel? Get(TId id)
    {
        var entity = Repository.Get(id);
        return entity == null ? null : Converter.ToModel(entity);
    }

    public async Task<TModel?> GetAsync(TId id)
    {
        var entity = await Repository.GetAsync(id);
        return entity == null ? null : Converter.ToModel(entity);
    }

    public void Create(TModel model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        Repository.Create(Converter.ToEntity(model));
        Repository.SaveChanges();
    }

    public Task CreateAsync(TModel model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        Repository.Create(Converter.ToEntity(model));
        return Repository.SaveChangesAsync();
    }

    public void Update(TId id, TModel model)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        
        Repository.Update(id, Converter.ToEntity(model));
        Repository.SaveChanges();
    }
    
    public async Task UpdateAsync(TId id, TModel model)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        
        await Repository.UpdateAsync(id, Converter.ToEntity(model));
        await Repository.SaveChangesAsync();
    }

    public void Delete(TId id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        
        Repository.Delete(id);
        Repository.SaveChanges();
    }
    
    public async Task DeleteAsync(TId id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        
        await Repository.DeleteAsync(id);
        await Repository.SaveChangesAsync();
    }
}