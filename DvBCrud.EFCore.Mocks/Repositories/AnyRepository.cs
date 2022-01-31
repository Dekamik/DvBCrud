using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.Mocks.Repositories
{
    [ExcludeFromCodeCoverage]
    public class AnyRepository : Repository<AnyEntity, int, AnyDbContext>, IAnyRepository
    {
        public AnyRepository(AnyDbContext context, ILogger<AnyRepository> logger) : base(context, logger)
        {

        }
    }
}
