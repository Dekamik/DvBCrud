using DvBCrud.EFCore.Entities;
using DvBCrud.EFCore.Repositories;

namespace DvBCrud.EFCore.Services;

public abstract class BaseService<TEntity, TId, TRepository, TModel, TConverter>
    where TEntity : BaseEntity<TId>
    where TRepository : IRepository<TEntity, TId>
    where TModel : BaseModel
    where TConverter : BaseConverter<TEntity, TModel>
{
    protected readonly TRepository Repository;
    protected readonly TConverter Converter;

    public BaseService(TRepository repository, TConverter converter)
    {
        Converter = converter;
        Repository = repository;
    }

    public IEnumerable<TModel> GetAll() => Repository.GetAll().Select(Converter.ToModel);

    public TModel Get(TId id) => Converter.ToModel(Repository.Get(id));

    public void Create(TModel model) => Repository.Create(Converter.ToEntity(model));

    public void Update(TId id, TModel model) => Repository.Update(id, Converter.ToEntity(model));

    public void Delete(TId id) => Repository.Delete(id);
}