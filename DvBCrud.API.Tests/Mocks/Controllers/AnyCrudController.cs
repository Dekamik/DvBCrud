using System.Diagnostics.CodeAnalysis;
using DvBCrud.Shared;

namespace DvBCrud.API.Tests.Mocks.Controllers;

[ExcludeFromCodeCoverage]
public class AnyCrudController : CrudController<string, AnyModel, IRepository<string, AnyModel>>
{
    public AnyCrudController(IRepository<string, AnyModel> repository) : base(repository)
    {
    }
}