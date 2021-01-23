using DvBCrud.EFCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.Repositories
{
    public interface IRepository<TEntity, TId> : IReadOnlyRepository<TEntity, TId> 
        where TEntity : BaseEntity<TId>
    {
        void Create(TEntity entity);

        void CreateRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        void Delete(TId id);

        void DeleteRange(IEnumerable<TId> ids);

        Task SaveChanges();
    }
}
