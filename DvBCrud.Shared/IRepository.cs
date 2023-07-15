using DvBCrud.Shared.Exceptions;

namespace DvBCrud.Shared;

/// <summary>
/// A repository for Creating, Reading, Updating and Deleting <typeparamref name="TModel"/> instances
/// </summary>
/// <typeparam name="TId"><typeparamref name="TModel"/> key type</typeparam>
/// <typeparam name="TModel">Model type</typeparam>
/// <typeparam name="TFilter">Filter type for <typeparamref name="TModel"/></typeparam>
public interface IRepository<TId, TModel, TFilter>
{
    /// <summary>
    /// Lists all entities
    /// </summary>
    /// <param name="filter">Filter to apply to entities</param>
    /// <returns>An <see cref="IQueryable"/> containing all <typeparamref name="TModel"/> instances</returns>
    IEnumerable<TModel> List(TFilter filter);

    /// <summary>
    /// Finds a single <typeparamref name="TModel"/> whose Id matches <paramref name="id"/>
    /// </summary>
    /// <param name="id">Key value to query</param>
    /// <returns>The matching <typeparamref name="TModel"/></returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
    /// <exception cref="NotFoundException">Thrown if <typeparamref name="TModel"/> with <paramref name="id"/> not found</exception>
    TModel Get(TId id);

    /// <summary>
    /// Finds a single <typeparamref name="TModel"/> whose Id matches <paramref name="id"/> asynchronously
    /// </summary>
    /// <param name="id">Key value to query</param>
    /// <returns>The matching <typeparamref name="TModel"/></returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
    /// <exception cref="NotFoundException">Thrown if <typeparamref name="TModel"/> with <paramref name="id"/> not found</exception>
    Task<TModel> GetAsync(TId id);

    /// <summary>
    /// Creates the specified <paramref name="model"/> and tracks it
    /// </summary>
    /// <param name="model">The entity to create</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="model"/> is null</exception>
    /// <exception cref="InvalidOperationException">Thrown if entity is already tracked</exception>
    TId Create(TModel model);
        
    /// <summary>
    /// Creates the specified <paramref name="entity"/> asynchronously and tracks it
    /// </summary>
    /// <param name="entity">The entity to create</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null</exception>
    Task<TId> CreateAsync(TModel entity);

    /// <summary>
    /// Finds the tracked <typeparamref name="TModel"/> by <paramref name="id"/> and copies values from the <paramref name="entity"/> to its corresponding tracked <typeparamref name="TModel"/>
    /// </summary>
    /// <param name="id">The Id of entity to update</param>
    /// <param name="entity">New entity data</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null</exception>
    /// <exception cref="NotFoundException">Thrown if <typeparamref name="TModel"/> with <paramref name="id"/> not found</exception>"
    void Update(TId id, TModel entity);

    /// <summary>
    /// Finds the tracked <typeparamref name="TModel"/> by <paramref name="id"/> asynchronously and copies values from the <paramref name="entity"/> to its corresponding tracked <typeparamref name="TModel"/>
    /// </summary>
    /// <param name="id">The Id of entity to update</param>
    /// <param name="entity">New entity data</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null</exception>
    /// <exception cref="NotFoundException">Thrown if <typeparamref name="TModel"/> with <paramref name="id"/> not found</exception>"
    Task UpdateAsync(TId id, TModel entity);

    /// <summary>
    /// Finds and removes the <typeparamref name="TModel"/> whose Id matches <paramref name="id"/>
    /// </summary>
    /// <param name="id">Id of <typeparamref name="TModel"/> to remove</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
    /// <exception cref="NotFoundException">Thrown if <typeparamref name="TModel"/> with <paramref name="id"/> not found</exception>"
    void Delete(TId id);

    /// <summary>
    /// Finds and removes the <typeparamref name="TModel"/> whose Id matches <paramref name="id"/> asynchronously
    /// </summary>
    /// <param name="id">Id of <typeparamref name="TModel"/> to remove</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
    /// <exception cref="NotFoundException">Thrown if <typeparamref name="TModel"/> with <paramref name="id"/> not found</exception>"
    Task DeleteAsync(TId id);
}