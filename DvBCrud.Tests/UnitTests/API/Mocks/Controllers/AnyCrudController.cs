using System.Diagnostics.CodeAnalysis;
using DvBCrud.API;
using DvBCrud.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.Tests.UnitTests.API.Mocks.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
[Route("api/v1/[controller]")]
public class AnyCrudController : CrudController<string, AnyModel, IRepository<string, AnyModel, AnyFilter>, AnyFilter>
{
    public AnyCrudController(IRepository<string, AnyModel, AnyFilter> repository) : base(repository)
    {
    }
}