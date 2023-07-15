using System.Diagnostics.CodeAnalysis;

namespace DvBCrud.EFCore.Tests.Mocks;

[ExcludeFromCodeCoverage]
public class AnyRepository : Repository<AnyEntity, string, AnyDbContext, AnyMapper, AnyModel, AnyFilter>, IAnyRepository
{
    public AnyRepository(AnyDbContext context, AnyMapper mapper) : base(context, mapper)
    {
    }
}