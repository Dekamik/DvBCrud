using DvBCrud.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.Repositories
{
    public class AuditedRepository<TEntity, TId, TUserId, TDbContext> : ReadOnlyRepository<TEntity, TId, TDbContext>, IAuditedRepository<TEntity, TId, TUserId>
        where TEntity : BaseAuditedEntity<TId, TUserId>
        where TDbContext : DbContext
    {
        public AuditedRepository(TDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {

        }

        public void Create(TEntity entity, TUserId userId)
        {
            throw new NotImplementedException();
        }

        public void CreateRange(TEntity entity, TUserId userId)
        {
            throw new NotImplementedException();
        }

        public void Delete(TId id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<TId> ids)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(TId id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetRange(IEnumerable<TId> ids)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity, TUserId userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(TEntity entity, TUserId userId)
        {
            throw new NotImplementedException();
        }
    }
}
