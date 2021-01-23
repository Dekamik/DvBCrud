using DvBCrud.EFCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.Repositories
{
    public interface IAuditedRepository<TEntity, TId, TUserId> : IReadOnlyRepository<TEntity, TId>
        where TEntity : BaseAuditedEntity<TId, TUserId>
    {
        void Create(TEntity entity, TUserId userId);

        void CreateRange(TEntity entity, TUserId userId);

        void Update(TEntity entity, TUserId userId);

        void UpdateRange(TEntity entity, TUserId userId);

        void Delete(TId id);

        void DeleteRange(IEnumerable<TId> ids);

        Task SaveChanges();
    }
}
