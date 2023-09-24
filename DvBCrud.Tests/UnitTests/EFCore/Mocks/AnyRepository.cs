using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore;

namespace DvBCrud.Tests.UnitTests.EFCore.Mocks;

[ExcludeFromCodeCoverage]
public class AnyRepository : Repository<AnyEntity, string, AnyDbContext, AnyMapper, AnyModel, AnyFilter>, IAnyRepository
{
    public AnyRepository(AnyDbContext context, AnyMapper mapper) : base(context, mapper)
    {
    }
}