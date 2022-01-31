using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.Mocks.Repositories
{
    [ExcludeFromCodeCoverage]
    public class AnyNullableIdRepository : Repository<AnyNullableIdEntity, string, AnyDbContext>
    {
        public AnyNullableIdRepository(AnyDbContext context, ILogger<AnyNullableIdRepository> logger) : base(context, logger)
        {

        }
    }
}
