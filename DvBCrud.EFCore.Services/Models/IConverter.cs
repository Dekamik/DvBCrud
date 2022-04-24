namespace DvBCrud.EFCore.Services.Models;

public interface IConverter<TEntity, TModel>
{
    TModel ToModel(TEntity entity);
    TEntity ToEntity(TModel model);
}