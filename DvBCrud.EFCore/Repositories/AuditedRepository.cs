using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.Repositories
{
    public class AuditedRepository<TEntity, TId, TUserId, TDbContext> : Repository<TEntity, TId, TDbContext>, IAuditedRepository<TEntity, TId, TUserId>
        where TEntity : BaseAuditedEntity<TId, TUserId>
        where TDbContext : DbContext
    {
        public LogLevel AuditLogLevel { get; set; } = LogLevel.Trace;

        public AuditedRepository(TDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {

        }

        public virtual void Create(TEntity entity, TUserId userId)
        {
            var now = DateTime.UtcNow;
            logger.Log(AuditLogLevel, $"User {userId} called {nameof(Create)} for a new {nameof(TEntity)} at {now}");

            entity.CreatedAt = now;
            entity.CreatedBy = userId;

            base.Create(entity);
        }

        public virtual void Update(TId id, TEntity entity, TUserId userId)
        {
            var now = DateTime.UtcNow;
            logger.Log(AuditLogLevel, $"User {userId} called {nameof(UpdateAsync)} for a {nameof(TEntity)} with Id {entity.Id} at {now}");

            entity.UpdatedAt = now;
            entity.UpdatedBy = userId;

            base.Update(id, entity);
        }

        public virtual Task UpdateAsync(TId id, TEntity entity, TUserId userId)
        {
            var now = DateTime.UtcNow;
            logger.Log(AuditLogLevel, $"User {userId} called {nameof(UpdateAsync)} for a {nameof(TEntity)} with Id {entity.Id} at {now}");

            entity.UpdatedAt = now;
            entity.UpdatedBy = userId;

            return base.UpdateAsync(id, entity);
        }

        #region Hidden methods

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use AuditedRepository.Create(TEntity, TUserId) instead")]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override void Create(TEntity entity)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            throw new NotSupportedException("Use AuditedRepository.Create(TEntity, TUserId) instead");
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use AuditedRepository.Update(TEntity, TUserId) instead")]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override void Update(TId id, TEntity entity)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            throw new NotSupportedException("Use AuditedRepository.Update(TEntity, TUserId) instead");
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use AuditedRepository.Update(TEntity, TUserId) instead")]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override Task UpdateAsync(TId id, TEntity entity)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            throw new NotSupportedException("Use AuditedRepository.Update(TEntity, TUserId) instead");
        }

        #endregion
    }
}
