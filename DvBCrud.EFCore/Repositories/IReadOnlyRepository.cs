using DvBCrud.EFCore.Entities;
using System.Linq;

namespace DvBCrud.EFCore.Repositories
{
    public interface IReadOnlyRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Get(params TId[] id);
    }
}
