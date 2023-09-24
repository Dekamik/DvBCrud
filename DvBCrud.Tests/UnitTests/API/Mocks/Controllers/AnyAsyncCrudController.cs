using System.Diagnostics.CodeAnalysis;
using DvBCrud.API;
using DvBCrud.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.Tests.UnitTests.API.Mocks.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
[Route("api/v1/[controller]")]
public class AnyAsyncCrudController : AsyncCrudController<string, AnyModel, IRepository<string, AnyModel, AnyFilter>, AnyFilter>
{
    public AnyAsyncCrudController(IRepository<string, AnyModel, AnyFilter> repository) : base(repository)
    {

    }
}