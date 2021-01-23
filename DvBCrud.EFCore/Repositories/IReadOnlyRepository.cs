using DvBCrud.EFCore.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DvBCrud.EFCore.Repositories
{
    /// <summary>
    /// A read-only repository for querying untracked <typeparamref name="TEntity"/> instances in database
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TId"><typeparamref name="TEntity"/> key type</typeparam>
    public interface IReadOnlyRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns>An <see cref="IQueryable"/> containing all <typeparamref name="TEntity"/> instances</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Finds a single <typeparamref name="TEntity"/> whose Id matches <paramref name="id"/>
        /// </summary>
        /// <param name="id">Key value to query</param>
        /// <returns>The matching <typeparamref name="TEntity"/> or null if it doesn't exist</returns>
        TEntity Get(TId id);

        /// <summary>
        /// Finds multiple <typeparamref name="TEntity"/> instances whose Ids are contained within <paramref name="ids"/>
        /// </summary>
        /// <param name="ids">Key values to query</param>
        /// <returns>An <see cref="IQueryable"/> containing all, some or no matching <typeparamref name="TEntity"/> instances</returns>
        IQueryable<TEntity> GetRange(IEnumerable<TId> ids);
    }
}
