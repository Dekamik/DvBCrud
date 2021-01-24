using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.Mocks.Repositories
{
    public class AnyReadOnlyRepository : ReadOnlyRepository<AnyEntity, int, AnyDbContext>
    {
        public AnyReadOnlyRepository(AnyDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {

        }
    }
}
