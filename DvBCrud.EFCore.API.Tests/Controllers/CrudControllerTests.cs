using System;
using System.Collections.Generic;
using System.Net;
using DvBCrud.EFCore.Mocks.Controllers;
using DvBCrud.EFCore.Mocks.Services;
using DvBCrud.EFCore.Mocks.Services.Model;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DvBCrud.EFCore.API.Tests.Controllers;

public class CrudControllerTests
{
    private readonly IAnyCrudHandler _crudHandler;
    private readonly AnyCrudController _controller;
    
    public CrudControllerTests()
    {
        _crudHandler = A.Fake<IAnyCrudHandler>();
        _controller = new AnyCrudController(_crudHandler);
    }

    [Fact]
    public void Create_AnyModel_ModelCreated()
    {
        var model = new AnyModel();

        _controller.Create(model);

        A.CallTo(() => _crudHandler.Create(model))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public void Create_AnyModel_ReturnsCreated()
    {
        var model = new AnyModel();

        var result = _controller.Create(model) as CreatedAtRouteResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.Created);
    }

    [Fact]
    public void Create_CreateNotAllowed_ReturnsForbidden()
    {
        var model = new AnyModel();
        var controller = new AnyReadOnlyController(A.Fake<IAnyCrudHandler>());

        var result = controller.Create(model) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public void Create_NullModel_ReturnsBadRequest()
    {
        A.CallTo(() => _crudHandler.Create(null))
            .Throws<ArgumentNullException>();

        var result = _controller.Create(null) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public void Read_ExistingId_ReturnsModel()
    {
        const string id = "1";
        var model = new AnyModel();
        
        A.CallTo(() => _crudHandler.Get(id))
            .Returns(model);

        var result = _controller.Read(id).Result as OkObjectResult;

        result.Should().NotBeNull();
        result!.Value.Should().Be(model);
    }
    
    [Fact]
    public void Read_ExistingId_ReturnsOk()
    {
        const string id = "1";
        var model = new AnyModel();
        
        A.CallTo(() => _crudHandler.Get(id))
            .Returns(model);

        var result = _controller.Read(id).Result as OkObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }
    
    [Fact]
    public void Read_NonExistingId_ReturnsNotFound()
    {
        const string id = "1";
        
        A.CallTo(() => _crudHandler.Get(id))
            .Returns(null);

        var result = _controller.Read(id).Result as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }
    
    [Fact]
    public void Read_ReadNotAllowed_ReturnsForbidden()
    {
        var controller = new AnyCreateUpdateController(_crudHandler);

        var result = controller.Read("1").Result as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.MethodNotAllowed);
    }
    
    [Fact]
    public void ReadAll_ExistingIds_ReturnsModels()
    {
        var models = new[]
        {
            new AnyModel(),
            new AnyModel()
        };
        
        A.CallTo(() => _crudHandler.List())
            .Returns(models);

        var result = _controller.ReadAll().Result as OkObjectResult;

        result.Should().NotBeNull();
        result!.Value.Should().Be(models);
    }
    
    [Fact]
    public void ReadAll_ExistingIds_ReturnsOk()
    {
        var models = new[]
        {
            new AnyModel(),
            new AnyModel()
        };
        
        A.CallTo(() => _crudHandler.List())
            .Returns(models);

        var result = _controller.ReadAll().Result as OkObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }
    
    [Fact]
    public void ReadAll_ReadNotAllowed_ReturnsForbidden()
    {
        var controller = new AnyCreateUpdateController(_crudHandler);

        var result = controller.ReadAll().Result as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public void Update_ExistingId_UpdatesModel()
    {
        const string id = "1";
        var model = new AnyModel();

        _controller.Update(id, model);

        A.CallTo(() => _crudHandler.Update(id, model))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public void Update_ExistingId_ReturnsNoContent()
    {
        const string id = "1";
        var model = new AnyModel();

        var result = _controller.Update(id, model) as NoContentResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
    }

    [Fact]
    public void Update_UpdateNotAllowed_ReturnsForbidden()
    {
        const string id = "1";
        var model = new AnyModel();
        var controller = new AnyReadOnlyController(A.Fake<IAnyCrudHandler>());

        var result = controller.Update(id, model) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public void Update_NonExistingId_ReturnsNotFound()
    {
        const string id = "1";
        var model = new AnyModel();
        A.CallTo(() => _crudHandler.Update(id, model))
            .Throws<KeyNotFoundException>();

        var result = _controller.Update(id, model) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public void Update_NullArguments_ReturnsBadRequest()
    {
        A.CallTo(() => _crudHandler.Update(null, null))
            .Throws<ArgumentNullException>();

        var result = _controller.Update(null, null) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public void Delete_ExistingId_ReturnsNoContent()
    {
        const string id = "1";

        var result = _controller.Delete(id) as NoContentResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
    }
    
    [Fact]
    public void Delete_ExistingId_DeletesModel()
    {
        const string id = "1";

        _controller.Delete(id);

        A.CallTo(() => _crudHandler.Delete(id))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Delete_DeleteForbidden_ReturnsForbidden()
    {
        const string id = "1";
        var controller = new AnyReadOnlyController(A.Fake<IAnyCrudHandler>());

        var result = controller.Delete(id) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public void Delete_NonExistingId_ReturnsNotFound()
    {
        const string id = "1";
        A.CallTo(() => _crudHandler.Delete(id))
            .Throws<KeyNotFoundException>();

        var result = _controller.Delete(id) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public void Delete_NullId_ReturnsBadRequest()
    {
        const string id = "1";
        A.CallTo(() => _crudHandler.Delete(id))
            .Throws<ArgumentNullException>();

        var result = _controller.Delete(id) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }
}