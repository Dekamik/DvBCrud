using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.Mocks.Repositories
{
    [ExcludeFromCodeCoverage]
    public class AnyAuditedRepository : AuditedRepository<AnyAuditedEntity, int, int, AnyDbContext>, IAnyAuditedRepository
    {
        public AnyAuditedRepository(AnyDbContext context, ILogger<AnyAuditedRepository> logger) : base(context, logger)
        {

        }
    }
}
