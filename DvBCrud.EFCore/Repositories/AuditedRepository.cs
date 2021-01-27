using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public virtual void CreateRange(IEnumerable<TEntity> entities, TUserId userId)
        {
            var now = DateTime.UtcNow;
            logger.Log(AuditLogLevel, $"User {userId} called {nameof(CreateRange)} for {entities.Count()} new {nameof(TEntity)} at {now}");

            foreach (TEntity entity in entities)
            {
                entity.CreatedAt = now;
                entity.CreatedBy = userId;
            }

            base.CreateRange(entities);
        }

        public virtual async Task Update(TEntity entity, TUserId userId, bool createIfNotExists = false)
        {
            var now = DateTime.UtcNow;
            logger.Log(AuditLogLevel, $"User {userId} called {nameof(Update)} for a {nameof(TEntity)} with Id {entity.Id} at {now}");

            entity.UpdatedAt = now;
            entity.UpdatedBy = userId;

            await base.Update(entity, createIfNotExists);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities, TUserId userId, bool createIfNotExists = false)
        {
            var now = DateTime.UtcNow;
            logger.Log(AuditLogLevel, $"User {userId} called {nameof(UpdateRange)} for {entities.Count()} {nameof(TEntity)} with Ids {string.Join(", ", entities)} at {now}");

            foreach (TEntity entity in entities)
            {
                entity.UpdatedAt = now;
                entity.UpdatedBy = userId;
            }

            base.UpdateRange(entities, createIfNotExists);
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
        [Obsolete("Use AuditedRepository.CreateRange(IEnumerable<TEntity>, TUserId) instead")]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override void CreateRange(IEnumerable<TEntity> entity)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            throw new NotSupportedException("Use AuditedRepository.CreateRange(IEnumerable<TEntity>, TUserId) instead");
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use AuditedRepository.Update(TEntity, TUserId) instead")]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override Task Update(TEntity entity, bool createIfNotExists = false)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            throw new NotSupportedException("Use AuditedRepository.Update(TEntity, TUserId) instead");
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use AuditedRepository.UpdateRange(IEnumerable<TEntity>, TUserId) instead")]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override void UpdateRange(IEnumerable<TEntity> entity, bool createIfNotExists = false)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            throw new NotSupportedException("Use AuditedRepository.UpdateRange(IEnumerable<TEntity>, TUserId) instead");
        }

        #endregion
    }
}
