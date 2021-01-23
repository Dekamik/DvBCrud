using DvBCrud.EFCore.Repositories;
using DvBCrud.EFCore.Tests.Mocks.DbContexts;
using DvBCrud.EFCore.Tests.Mocks.Entities;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.Tests.Mocks.Repositories
{
    public class AnyReadOnlyRepository : ReadOnlyRepository<AnyEntity, int, AnyDbContext>
    {
        public AnyReadOnlyRepository(AnyDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {

        }
    }
}
