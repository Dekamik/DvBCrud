using System.Diagnostics.CodeAnalysis;
using DvBCrud.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.API.Tests.Mocks.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
[Route("api/v1/[controller]")]
public class AnyCrudController : CrudController<string, AnyModel, IRepository<string, AnyModel>>
{
    public AnyCrudController(IRepository<string, AnyModel> repository) : base(repository)
    {
    }
}