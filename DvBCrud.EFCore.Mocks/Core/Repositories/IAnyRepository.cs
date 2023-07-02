using DvBCrud.EFCore.Mocks.Core.Entities;
using DvBCrud.EFCore.Mocks.Services.Model;
using DvBCrud.EFCore.Repositories;

namespace DvBCrud.EFCore.Mocks.Core.Repositories
{
    public interface IAnyRepository : IRepository<string, AnyModel>
    {
    }
}
