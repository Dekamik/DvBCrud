using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DvBCrud.EFCore.Mocks.Controllers;
using DvBCrud.EFCore.Mocks.Services;
using DvBCrud.EFCore.Mocks.Services.Model;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DvBCrud.EFCore.API.Tests.Controllers;

public class AsyncCrudControllerTests
{
    private readonly IAnyService _service;
    private readonly AnyAsyncCrudController _controller;
    
    public AsyncCrudControllerTests()
    {
        _service = A.Fake<IAnyService>();
        _controller = new AnyAsyncCrudController(_service);
    }

    [Fact]
    public async Task CreateAsync_AnyModel_ModelCreated()
    {
        var model = new AnyModel();

        await _controller.Create(model);

        A.CallTo(() => _service.CreateAsync(model))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task Create_AnyModel_ReturnsCreated()
    {
        var model = new AnyModel();

        var result = await _controller.Create(model) as CreatedAtRouteResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.Created);
    }

    [Fact]
    public async Task Create_CreateNotAllowed_ReturnsForbidden()
    {
        var model = new AnyModel();
        var controller = new AnyAsyncReadOnlyController(A.Fake<IAnyService>());

        var result = await controller.Create(model) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public async Task Create_NullModel_ReturnsBadRequest()
    {
        A.CallTo(() => _service.CreateAsync(null))
            .Throws<ArgumentNullException>();

        var result = await _controller.Create(null) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Read_ExistingId_ReturnsModel()
    {
        const string id = "1";
        var model = new AnyModel();
        
        A.CallTo(() => _service.GetAsync(id))
            .Returns(Task.FromResult((AnyModel?)model));

        var result = (await _controller.Read(id)).Result as OkObjectResult;

        result.Should().NotBeNull();
        result!.Value.Should().Be(model);
    }
    
    [Fact]
    public async Task Read_ExistingId_ReturnsOk()
    {
        const string id = "1";
        var model = new AnyModel();
        
        A.CallTo(() => _service.GetAsync(id))
            .Returns(Task.FromResult((AnyModel?)model));

        var result = (await _controller.Read(id)).Result as OkObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task Read_NonExistingId_ReturnsNotFound()
    {
        const string id = "1";
        
        A.CallTo(() => _service.GetAsync(id))
            .Returns(Task.FromResult((AnyModel?)null));

        var result = (await _controller.Read(id)).Result as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task Read_ReadNotAllowed_ReturnsForbidden()
    {
        var controller = new AnyAsyncCreateUpdateController(_service);

        var result = (await controller.Read("1")).Result as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.MethodNotAllowed);
    }
    
    [Fact]
    public async Task ReadAll_ExistingIds_ReturnsModels()
    {
        var models = new[]
        {
            new AnyModel(),
            new AnyModel()
        };
        
        A.CallTo(() => _service.List())
            .Returns(models);

        var result = (await _controller.ReadAll()).Result as OkObjectResult;

        result.Should().NotBeNull();
        result!.Value.Should().Be(models);
    }
    
    [Fact]
    public async Task ReadAll_ExistingIds_ReturnsOk()
    {
        var models = new[]
        {
            new AnyModel(),
            new AnyModel()
        };
        
        A.CallTo(() => _service.List())
            .Returns(models);

        var result = (await _controller.ReadAll()).Result as OkObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task ReadAll_ReadNotAllowed_ReturnsForbidden()
    {
        var controller = new AnyAsyncCreateUpdateController(_service);

        var result = (await controller.ReadAll()).Result as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public async Task Update_ExistingId_UpdatesModel()
    {
        const string id = "1";
        var model = new AnyModel();

        await _controller.Update(id, model);

        A.CallTo(() => _service.UpdateAsync(id, model))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task Update_ExistingId_ReturnsNoContent()
    {
        const string id = "1";
        var model = new AnyModel();

        var result = await _controller.Update(id, model) as NoContentResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Update_UpdateNotAllowed_ReturnsForbidden()
    {
        const string id = "1";
        var model = new AnyModel();
        var controller = new AnyAsyncReadOnlyController(A.Fake<IAnyService>());

        var result = await controller.Update(id, model) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public async Task Update_NonExistingId_ReturnsNotFound()
    {
        const string id = "1";
        var model = new AnyModel();
        A.CallTo(() => _service.UpdateAsync(id, model))
            .Throws<KeyNotFoundException>();

        var result = await _controller.Update(id, model) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Update_NullArguments_ReturnsBadRequest()
    {
        A.CallTo(() => _service.UpdateAsync(null, null))
            .Throws<ArgumentNullException>();

        var result = await _controller.Update(null, null) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Delete_ExistingId_ReturnsNoContent()
    {
        const string id = "1";

        var result = await _controller.Delete(id) as NoContentResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task Delete_ExistingId_DeletesModel()
    {
        const string id = "1";

        await _controller.Delete(id);

        A.CallTo(() => _service.DeleteAsync(id))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Delete_DeleteForbidden_ReturnsForbidden()
    {
        const string id = "1";
        var controller = new AnyAsyncReadOnlyController(A.Fake<IAnyService>());

        var result = await controller.Delete(id) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public async Task Delete_NonExistingId_ReturnsNotFound()
    {
        const string id = "1";
        A.CallTo(() => _service.DeleteAsync(id))
            .Throws<KeyNotFoundException>();

        var result = await _controller.Delete(id) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_NullId_ReturnsBadRequest()
    {
        const string id = "1";
        A.CallTo(() => _service.DeleteAsync(id))
            .Throws<ArgumentNullException>();

        var result = await _controller.Delete(id) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }
}