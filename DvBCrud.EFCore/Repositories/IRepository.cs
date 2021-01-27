using DvBCrud.EFCore.Entities;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.Repositories
{
    /// <summary>
    /// A repository for Creating, Reading, Updating and Deleting <typeparamref name="TEntity"/> instances
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TId"><typeparamref name="TEntity"/> key type</typeparam>
    public interface IRepository<TEntity, TId> : IReadOnlyRepository<TEntity, TId> 
        where TEntity : BaseEntity<TId>
    {
        /// <summary>
        /// Creates the specified <paramref name="entity"/> and tracks it
        /// </summary>
        /// <param name="entity">The entity to create</param>
        void Create(TEntity entity);

        /// <summary>
        /// Finds the tracked <typeparamref name="TEntity"/> by <paramref name="id"/> and copies values from the <paramref name="entity"/> to its corresponding tracked <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="id">The Id of entity to update</param>
        /// <param name="entity">New entity data</param>
        void Update(TId id, TEntity entity);

        /// <summary>
        /// Finds the tracked <typeparamref name="TEntity"/> by <paramref name="id"/> asynchronously and copies values from the <paramref name="entity"/> to its corresponding tracked <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="id">The Id of entity to update</param>
        /// <param name="entity">New entity data</param>
        Task UpdateAsync(TId id, TEntity entity);

        /// <summary>
        /// Finds and removes the <typeparamref name="TEntity"/> whose Id matches <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of <typeparamref name="TEntity"/> to remove</param>
        void Delete(TId id);

        /// <summary>
        /// Persists modifications to DbContext
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Persists modifications to DbContext asynchronously
        /// </summary>
        Task SaveChangesAsync();
    }
}
