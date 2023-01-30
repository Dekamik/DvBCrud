using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Mocks.Core.Entities;
using DvBCrud.EFCore.Mocks.Core.Repositories;
using DvBCrud.EFCore.Mocks.Services.Model;
using DvBCrud.EFCore.Services;

namespace DvBCrud.EFCore.Mocks.Services;

[ExcludeFromCodeCoverage]
public class AnyService : Service<AnyEntity, string, IAnyRepository, AnyModel, IAnyMapper>
{
    public AnyService(IAnyRepository repository, IAnyMapper mapper) : base(repository, mapper)
    {
    }
}