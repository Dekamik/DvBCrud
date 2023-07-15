using System.Diagnostics.CodeAnalysis;
using DvBCrud.Shared;
using DvBCrud.Shared.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.API.Tests.Mocks.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
[Route("api/v1/[controller]")]
[AllowedActions(CrudActions.Read)]
public class AnyAsyncReadOnlyController : AsyncCrudController<string, AnyModel, IRepository<string, AnyModel, AnyFilter>, AnyFilter>
{
    public AnyAsyncReadOnlyController(IRepository<string, AnyModel, AnyFilter> repository) : base(repository)
    {
    }
}