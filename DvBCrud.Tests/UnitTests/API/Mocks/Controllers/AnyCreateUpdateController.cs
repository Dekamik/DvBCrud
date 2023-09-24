using System.Diagnostics.CodeAnalysis;
using DvBCrud.API;
using DvBCrud.Shared;
using DvBCrud.Shared.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.Tests.UnitTests.API.Mocks.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
[Route("api/v1/[controller]")]
[AllowedActions(CrudActions.Create | CrudActions.Update)]
public class AnyCreateUpdateController : CrudController<string, AnyModel, IRepository<string, AnyModel, AnyFilter>, AnyFilter>
{
    public AnyCreateUpdateController(IRepository<string, AnyModel, AnyFilter> repository) : base(repository)
    {
    }
}