namespace DvBCrud.EFCore.Services;

public abstract class BaseConverter<TEntity, TModel>
{
    public abstract TModel ToModel(TEntity entity);

    public abstract TEntity ToEntity(TModel model);
}