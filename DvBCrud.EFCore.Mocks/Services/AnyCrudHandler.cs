using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Handlers;
using DvBCrud.EFCore.Mocks.Core.Entities;
using DvBCrud.EFCore.Mocks.Core.Repositories;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Services;

[ExcludeFromCodeCoverage]
public class AnyCrudHandler : CrudHandler<string, IAnyRepository, AnyModel>
{
    public AnyCrudHandler(IAnyRepository repository) : base(repository)
    {
    }
}