using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.Repositories
{
    public abstract class AuditedRepository<TEntity, TId, TUserId, TDbContext> : Repository<TEntity, TId, TDbContext>, IAuditedRepository<TEntity, TId, TUserId>
        where TEntity : BaseAuditedEntity<TId, TUserId>
        where TDbContext : DbContext
    {
        public AuditedRepository(TDbContext context, ILogger<AuditedRepository<TEntity, TId, TUserId, TDbContext>> logger) : base(context, logger)
        {

        }

        public virtual void Create(TEntity entity, TUserId userId)
        {
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.CreatedBy = userId;

            base.Create(entity);
        }

        public virtual void Update(TId id, TEntity entity, TUserId userId)
        {
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedBy = userId;

            base.Update(id, entity);
        }

        public virtual Task UpdateAsync(TId id, TEntity entity, TUserId userId)
        {
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedBy = userId;

            return base.UpdateAsync(id, entity);
        }

        #region Hidden methods

        /// <summary>
        /// Obsolete, use <see cref="Create(TEntity, TUserId)"/> instead.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown since this method is not supported</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use AuditedRepository.Create(TEntity, TUserId) instead")]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override void Create(TEntity entity)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            throw new NotSupportedException("Use AuditedRepository.Create(TEntity, TUserId) instead");
        }

        /// <summary>
        /// Obsolete, use <see cref="Update(TId, TEntity, TUserId)"/> instead.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown since this method is not supported</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use AuditedRepository.Update(TId, TEntity, TUserId) instead")]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override void Update(TId id, TEntity entity)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            throw new NotSupportedException("Use AuditedRepository.Update(TId, TEntity, TUserId) instead");
        }

        /// <summary>
        /// Obsolete, use <see cref="UpdateAsync(TId, TEntity, TUserId)"/> instead.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown since this method is not supported</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use AuditedRepository.UpdateAsync(TId, TEntity, TUserId) instead")]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override Task UpdateAsync(TId id, TEntity entity)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            throw new NotSupportedException("Use AuditedRepository.Update(TId, TEntity, TUserId) instead");
        }

        #endregion
    }
}
