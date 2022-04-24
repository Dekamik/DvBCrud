using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Mocks.Core.DbContexts;
using DvBCrud.EFCore.Mocks.Core.Entities;
using DvBCrud.EFCore.Repositories;

namespace DvBCrud.EFCore.Mocks.Core.Repositories
{
    [ExcludeFromCodeCoverage]
    public class AnyRepository : Repository<AnyEntity, string, AnyDbContext>, IAnyRepository
    {
        public AnyRepository(AnyDbContext context) : base(context)
        {

        }
    }
}
