using DvBCrud.EFCore.Entities;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.Repositories
{
    public interface IRepository<TEntity, TId> : IReadOnlyRepository<TEntity, TId> 
        where TEntity : BaseEntity<TId>
    {
        void Create(params TEntity[] entity);

        void Update(params TEntity[] entity);

        void Delete(params TId[] id);

        Task SaveChanges();
    }
}
