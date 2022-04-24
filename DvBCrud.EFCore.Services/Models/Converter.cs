namespace DvBCrud.EFCore.Services.Models;

public abstract class Converter<TEntity, TModel> : IConverter<TEntity, TModel>
{
    public abstract TModel ToModel(TEntity entity);

    public abstract TEntity ToEntity(TModel model);
}