using DvBCrud.API;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.Tests.UnitTests.API.Mocks.Controllers;

public class AnyCrudControllerBase : CrudControllerBase<string>
{
    public IActionResult AnyNotAllowedMethod()
    {
        return NotAllowed("GET");
    }
}