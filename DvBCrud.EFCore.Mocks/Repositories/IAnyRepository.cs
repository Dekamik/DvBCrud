using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Repositories;

namespace DvBCrud.EFCore.Mocks.Repositories
{
    public interface IAnyRepository : IRepository<AnyEntity, int>
    {
    }
}
