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
    /// <param name="other">The entity to convert</param>
    /// <returns>The converted model</returns>
    TModel ToModel(TEntity other);
    
    /// <summary>
    /// Convert from <typeparamref name="TModel"/> to <typeparamref name="TEntity"/>
    /// </summary>
    /// <param name="other">The model to convert</param>
    /// <returns>The converted entity</returns>
    TEntity ToEntity(TModel other);
    
    /// <summary>
    /// Update <paramref name="target"/> entity with values from <paramref name="other"/>
    /// </summary>
    /// <param name="other">Entity to copy from</param>
    /// <param name="target">Entity to copy to</param>
    void UpdateEntity(TEntity target, TEntity other);
}