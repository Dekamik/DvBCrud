using DvBCrud.EFCore.Entities;
using System.Collections.Generic;
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
        /// Creates all specified <paramref name="entities"/> and tracks them
        /// </summary>
        /// <param name="entities">The entities to create</param>
        void CreateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Finds the tracked <typeparamref name="TEntity"/> by Id and copies values from the <paramref name="entity"/> to its corresponding tracked <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="entity">New entity data</param>
        /// <param name="createIfNotExists">If true, creates <paramref name="entity"/> if it isn't found. <see cref="SaveChanges"/> should be called after invocation if used.</param>
        Task Update(TEntity entity, bool createIfNotExists = false);

        /// <summary>
        /// Finds tracked <typeparamref name="TEntity"/> instances by Id and copies values from each <paramref name="entities"/> to their corresponding tracked <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="entities">New entity data</param>
        /// <param name="createIfNotExists">If true, creates all <paramref name="entities"/> that aren't found. <see cref="SaveChanges"/> should be called after invocation if used.</param>
        void UpdateRange(IEnumerable<TEntity> entities, bool createIfNotExists = false);

        /// <summary>
        /// Finds and removes the <typeparamref name="TEntity"/> whose Id matches <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of <typeparamref name="TEntity"/> to remove</param>
        void Delete(TId id);

        /// <summary>
        /// Finds and removes all <typeparamref name="TEntity"/> instances whose Ids matches those found in <paramref name="ids"/>
        /// </summary>
        /// <param name="ids">Ids of all <typeparamref name="TEntity"/> instances to remove</param>
        void DeleteRange(IEnumerable<TId> ids);

        /// <summary>
        /// Persists modifications to DbContext
        /// </summary>
        /// <returns>An async <see cref="Task"/></returns>
        Task SaveChanges();
    }
}
