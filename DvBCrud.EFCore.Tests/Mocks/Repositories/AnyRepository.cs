using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore;
using DvBCrud.Mocks.Core.DbContexts;
using DvBCrud.Mocks.Core.Entities;
using DvBCrud.Mocks.Model;

namespace DvBCrud.Mocks.Core.Repositories
{
    [ExcludeFromCodeCoverage]
    public class AnyRepository : Repository<AnyEntity, string, AnyDbContext, AnyMapper, AnyModel>, IAnyRepository
    {
        public AnyRepository(AnyDbContext context, AnyMapper mapper) : base(context, mapper)
        {
        }
    }
}
