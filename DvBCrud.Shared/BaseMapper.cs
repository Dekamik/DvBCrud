using DvBCrud.Shared.Filtering;

namespace DvBCrud.Shared;

/// <summary>
/// Base class for implementing conversion logic between <typeparamref name="TEntity"/> and <typeparamref name="TModel"/>.
/// </summary>
/// <typeparam name="TEntity">Entity/data model type</typeparam>
/// <typeparam name="TModel">API model type</typeparam>
/// <typeparam name="TFilter"></typeparam>
public abstract class BaseMapper<TEntity, TModel, TFilter>
{
    /// <summary>
    /// Convert from <typeparamref name="TEntity"/> to <typeparamref name="TModel"/> 
    /// </summary>
    /// <param name="other">The entity to convert</param>
    /// <returns>The converted model</returns>
    public abstract TModel ToModel(TEntity other);
    
    /// <summary>
    /// Convert from <typeparamref name="TModel"/> to <typeparamref name="TEntity"/>
    /// </summary>
    /// <param name="other">The model to convert</param>
    /// <returns>The converted entity</returns>
    public abstract TEntity ToEntity(TModel other);
    
    /// <summary>
    /// Update <paramref name="target"/> entity with values from <paramref name="other"/>
    /// </summary>
    /// <param name="other">Entity to copy from</param>
    /// <param name="target">Entity to copy to</param>
    public abstract void UpdateEntity(TEntity target, TEntity other);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities">IEnumerable of entities to filter</param>
    /// <param name="filter">Filter to apply to <paramref name="entities"/></param>
    /// <returns></returns>
    public virtual IEnumerable<TEntity> FilterAndSort(IEnumerable<TEntity> entities, TFilter filter)
    {
        if (filter is IPaginateFilter paginateFilter)
            entities = entities.Skip(paginateFilter.Skip)
                .Take(paginateFilter.Take);

        return entities;
    }
}