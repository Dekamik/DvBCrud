using DvBCrud.EFCore.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.EFCore.API.Tests.Controllers.Mocks;

public class AnyCrudControllerBase : CrudControllerBase<string>
{
    public IActionResult AnyNotAllowedMethod()
    {
        return NotAllowed("GET");
    }
}