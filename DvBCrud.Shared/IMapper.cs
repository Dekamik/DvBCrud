namespace DvBCrud.Shared;

/// <summary>
/// Base class for implementing conversion logic between <typeparamref name="TEntity"/> and <typeparamref name="TModel"/>.
/// </summary>
/// <typeparam name="TEntity">Entity/data model type</typeparam>
/// <typeparam name="TModel">API model type</typeparam>
public interface IMapper<TEntity, TModel>
{
    /// <summary>
    /// Convert from <typeparamref name="TEntity"/> to <typeparamref name="TModel"/> 
    /// </summary>
    /// <param name="entity">The entity to convert</param>
    /// <returns>The converted model</returns>
    TModel ToModel(TEntity entity);
    
    /// <summary>
    /// Convert from <typeparamref name="TModel"/> to <typeparamref name="TEntity"/>
    /// </summary>
    /// <param name="entity">The model to convert</param>
    /// <returns>The converted entity</returns>
    TEntity ToEntity(TModel entity);
    
    /// <summary>
    /// Update <paramref name="destination"/> entity with values from <paramref name="source"/>
    /// </summary>
    /// <param name="source">Entity to copy from</param>
    /// <param name="destination">Entity to copy to</param>
    void UpdateEntity(TEntity destination, TEntity source);
}