using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.Mocks.Repositories
{
    public class AnyRepository : Repository<AnyEntity, int, AnyDbContext>
    {
        public AnyRepository(AnyDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {

        }
    }
}
