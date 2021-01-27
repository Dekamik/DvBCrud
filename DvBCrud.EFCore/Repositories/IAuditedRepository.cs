using DvBCrud.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.Repositories
{
    /// <summary>
    /// An audited repository for Creating, Reading, Updating and Deleting <typeparamref name="TEntity"/> instances.
    /// Create and Update actions will be logged onto the entity with UserId and Timestamp
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TId"><see cref="TEntity"/> key type</typeparam>
    /// <typeparam name="TUserId">User ID type</typeparam>
    public interface IAuditedRepository<TEntity, TId, TUserId> : IRepository<TEntity, TId>
        where TEntity : BaseAuditedEntity<TId, TUserId>
    {
        /// <summary>
        /// Creates the specified <typeparamref name="TEntity"/>, tracks it and sets CreatedBy to <paramref name="userId"/> and CreatedAt to <see cref="DateTime.UtcNow"/>
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> to create</param>
        /// <param name="userId">User ID</param>
        void Create(TEntity entity, TUserId userId);

        /// <summary>
        /// Creates all <typeparamref name="TEntity"/> in <paramref name="entities"/>, tracks them and sets CreatedBy to <paramref name="userId"/> and CreatedAt to <see cref="DateTime.UtcNow"/> for each <typeparamref name="TEntity"/> in <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">List of <see cref="TEntity"/> to create</param>
        /// <param name="userId">User ID</param>
        void CreateRange(IEnumerable<TEntity> entities, TUserId userId);

        /// <summary>
        /// Updates the specified <typeparamref name="TEntity"/> if found in database and sets UpdatedBy to <paramref name="userId"/> and UpdatedAt to <see cref="DateTime.UtcNow"/>
        /// </summary>
        /// <param name="entity">New entity data</param>
        /// <param name="userId">User ID</param>
        /// <param name="createIfNotExists">If true, creates <paramref name="entity"/> if it isn't found. <see cref="SaveChanges"/> should be called after invocation if used.</param>
        Task UpdateAsync(TEntity entity, TUserId userId, bool createIfNotExists = false);

        /// <summary>
        /// Updates the <typeparamref name="TEntity"/> in <paramref name="entities"/> found in database and sets UpdatedBy to <paramref name="userId"/> and UpdatedAt to <see cref="DateTime.UtcNow"/> for each <typeparamref name="TEntity"/> in <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">New entity data</param>
        /// <param name="userId">User ID</param>
        /// <param name="createIfNotExists">If true, creates <paramref name="entity"/> if it isn't found. <see cref="SaveChanges"/> should be called after invocation if used.</param>
        void UpdateRange(IEnumerable<TEntity> entities, TUserId userId, bool createIfNotExists = false);
    }
}
