using DvBCrud.EFCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.Repositories
{
    public interface IAuditedRepository<TEntity, TId, TUserId> : IRepository<TEntity, TId>
        where TEntity : BaseAuditedEntity<TId, TUserId>
    {
        void Create(TEntity entity, TUserId userId);

        void CreateRange(IEnumerable<TEntity> entities, TUserId userId);

        void Update(TEntity entity, TUserId userId, bool createIfNotExists = false);

        void UpdateRange(IEnumerable<TEntity> entities, TUserId userId, bool createIfNotExists = false);
    }
}
