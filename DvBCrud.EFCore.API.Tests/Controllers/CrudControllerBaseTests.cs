using DvBCrud.EFCore.API.Tests.Controllers.Mocks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DvBCrud.EFCore.API.Tests.Controllers;

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