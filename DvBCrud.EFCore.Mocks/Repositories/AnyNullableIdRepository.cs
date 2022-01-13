using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.Mocks.Repositories
{
    public class AnyNullableIdRepository : Repository<AnyNullableIdEntity, string, AnyDbContext>
    {
        public AnyNullableIdRepository(AnyDbContext context, ILogger logger) : base(context, logger)
        {

        }
    }
}
