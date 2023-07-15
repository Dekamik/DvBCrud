using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Tests.Mocks.Mappers;

namespace DvBCrud.EFCore.Tests.Mocks.Repositories;

[ExcludeFromCodeCoverage]
public class AnyRepository : Repository<AnyEntity, string, AnyDbContext, AnyMapper, AnyModel, AnyFilter>, IAnyRepository
{
    public AnyRepository(AnyDbContext context, AnyMapper mapper) : base(context, mapper)
    {
    }
}