using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.Mocks.Repositories
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class AnyReadOnlyRepository : ReadOnlyRepository<AnyEntity, int, AnyDbContext>, IAnyReadOnlyRepository
#pragma warning restore CS0618 // Type or member is obsolete
    {
        public AnyReadOnlyRepository(AnyDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {

        }
    }
}
