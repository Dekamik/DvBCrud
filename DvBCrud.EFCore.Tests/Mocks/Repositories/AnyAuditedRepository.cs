using DvBCrud.EFCore.Repositories;
using DvBCrud.EFCore.Tests.Mocks.DbContexts;
using DvBCrud.EFCore.Tests.Mocks.Entities;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.Tests.Mocks.Repositories
{
    public class AnyAuditedRepository : AuditedRepository<AnyAuditedEntity, int, int, AnyDbContext>
    {
        public AnyAuditedRepository(AnyDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {

        }
    }
}
