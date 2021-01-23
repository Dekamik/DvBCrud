using DvBCrud.EFCore.Repositories;
using DvBCrud.EFCore.Tests.Mocks.DbContexts;
using DvBCrud.EFCore.Tests.Mocks.Entities;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.Tests.Mocks.Repositories
{
    public class AnyRepository : Repository<AnyEntity, int, AnyDbContext>
    {
        public AnyRepository(AnyDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {

        }
    }
}
