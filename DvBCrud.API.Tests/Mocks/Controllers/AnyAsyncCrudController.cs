using System.Diagnostics.CodeAnalysis;
using DvBCrud.Shared;

namespace DvBCrud.API.Tests.Mocks.Controllers;

[ExcludeFromCodeCoverage]
public class AnyAsyncCrudController : AsyncCrudController<string, AnyModel, IRepository<string,AnyModel>>
{
    public AnyAsyncCrudController(IRepository<string,AnyModel> repository) : base(repository)
    {

    }
}