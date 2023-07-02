using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Mocks.Core.DbContexts;
using DvBCrud.EFCore.Mocks.Core.Entities;
using DvBCrud.EFCore.Mocks.Services.Model;
using DvBCrud.EFCore.Repositories;

namespace DvBCrud.EFCore.Mocks.Core.Repositories
{
    [ExcludeFromCodeCoverage]
    public class AnyRepository : Repository<AnyEntity, string, AnyDbContext, AnyMapper, AnyModel>, IAnyRepository
    {
        public AnyRepository(AnyDbContext context, AnyMapper mapper) : base(context, mapper)
        {
        }
    }
}
