using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.API.Tests.Mocks.Controllers;

public class AnyCrudControllerBase : CrudControllerBase<string>
{
    public IActionResult AnyNotAllowedMethod()
    {
        return NotAllowed("GET");
    }
}