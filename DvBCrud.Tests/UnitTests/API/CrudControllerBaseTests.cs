using DvBCrud.Tests.UnitTests.API.Mocks.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.Tests.UnitTests.API;

public class CrudControllerBaseTests
{
    [Fact]
    public void NotAllowed_Any_Returns403ForbiddenResult()
    {
        var controller = new AnyCrudControllerBase();

        var result = (ObjectResult) controller.AnyNotAllowedMethod();

        result.StatusCode.Should().Be(405);
        result.Value.Should().Be("GET not allowed on String");
    }
}