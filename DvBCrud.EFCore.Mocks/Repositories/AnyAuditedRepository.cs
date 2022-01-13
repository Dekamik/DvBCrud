using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.Mocks.Repositories
{
    public class AnyAuditedRepository : AuditedRepository<AnyAuditedEntity, int, int, AnyDbContext>, IAnyAuditedRepository
    {
        public AnyAuditedRepository(AnyDbContext context, ILogger logger) : base(context, logger)
        {

        }
    }
}
