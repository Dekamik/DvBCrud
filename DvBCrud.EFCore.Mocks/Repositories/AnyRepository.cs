using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.Mocks.Repositories
{
    public class AnyRepository : Repository<AnyEntity, int, AnyDbContext>, IAnyRepository
    {
        public AnyRepository(AnyDbContext context, ILogger logger) : base(context, logger)
        {

        }
    }
}
