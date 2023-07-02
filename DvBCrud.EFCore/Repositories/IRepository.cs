using DvBCrud.EFCore.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.Repositories
{
    /// <summary>
    /// A repository for Creating, Reading, Updating and Deleting <typeparamref name="TEntity"/> instances
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TId"><typeparamref name="TEntity"/> key type</typeparam>
    public interface IRepository<TEntity, TId> 
        where TEntity : BaseEntity<TId>
    {
        /// <summary>
        /// Lists all entities
        /// </summary>
        /// <returns>An <see cref="IQueryable"/> containing all <typeparamref name="TEntity"/> instances</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        IQueryable<TEntity> List();

        /// <summary>
        /// Finds a single <typeparamref name="TEntity"/> whose Id matches <paramref name="id"/>
        /// </summary>
        /// <param name="id">Key value to query</param>
        /// <returns>The matching <typeparamref name="TEntity"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        TEntity? Get(TId id);

        /// <summary>
        /// Finds a single <typeparamref name="TEntity"/> whose Id matches <paramref name="id"/> asynchronously
        /// </summary>
        /// <param name="id">Key value to query</param>
        /// <returns>The matching <typeparamref name="TEntity"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        Task<TEntity?> GetAsync(TId id);

        /// <summary>
        /// Creates the specified <paramref name="entity"/> and tracks it
        /// </summary>
        /// <param name="entity">The entity to create</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null</exception>
        void Create(TEntity entity);

        /// <summary>
        /// Finds the tracked <typeparamref name="TEntity"/> by <paramref name="id"/> and copies values from the <paramref name="entity"/> to its corresponding tracked <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="id">The Id of entity to update</param>
        /// <param name="entity">New entity data</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null</exception>
        /// <exception cref="KeyNotFoundException">Thrown if <typeparamref name="TEntity"/> with <paramref name="id"/> not found</exception>"
        void Update(TId id, TEntity entity);

        /// <summary>
        /// Finds the tracked <typeparamref name="TEntity"/> by <paramref name="id"/> asynchronously and copies values from the <paramref name="entity"/> to its corresponding tracked <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="id">The Id of entity to update</param>
        /// <param name="entity">New entity data</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null</exception>
        /// <exception cref="KeyNotFoundException">Thrown if <typeparamref name="TEntity"/> with <paramref name="id"/> not found</exception>"
        Task UpdateAsync(TId id, TEntity entity);

        /// <summary>
        /// Finds and removes the <typeparamref name="TEntity"/> whose Id matches <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of <typeparamref name="TEntity"/> to remove</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        /// <exception cref="KeyNotFoundException">Thrown if <typeparamref name="TEntity"/> with <paramref name="id"/> not found</exception>"
        void Delete(TId id);

        /// <summary>
        /// Finds and removes the <typeparamref name="TEntity"/> whose Id matches <paramref name="id"/> asynchronously
        /// </summary>
        /// <param name="id">Id of <typeparamref name="TEntity"/> to remove</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id"/> is null</exception>
        /// <exception cref="KeyNotFoundException">Thrown if <typeparamref name="TEntity"/> with <paramref name="id"/> not found</exception>"
        Task DeleteAsync(TId id);

        /// <summary>
        /// Persists modifications to DbContext
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Persists modifications to DbContext asynchronously
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Checks whether or not a specific <typeparamref name="TEntity" exists/>
        /// </summary>
        /// <param name="id">Id of <typeparamref name="TEntity"/> to check</param>
        /// <returns>true if exists, otherwise false</returns>
        bool Exists(TId id);
    }
}
