using System;
using System.Collections.Generic;
using System.Net;
using DvBCrud.API.Tests.Mocks;
using DvBCrud.API.Tests.Mocks.Controllers;
using DvBCrud.Shared;
using DvBCrud.Shared.Exceptions;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

#pragma warning disable CS8625

namespace DvBCrud.API.Tests;

public class CrudControllerTests
{
    private readonly IRepository<string, AnyModel> _repository;
    private readonly AnyCrudController _controller;
    
    public CrudControllerTests()
    {
        _repository = A.Fake<IRepository<string, AnyModel>>();
        _controller = new AnyCrudController(_repository);
    }

    [Fact]
    public void Create_AnyModel_ModelCreated()
    {
        var model = new AnyModel();

        _controller.Create(model);

        A.CallTo(() => _repository.Create(model))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public void Create_AnyModel_ReturnsCreated()
    {
        var model = new AnyModel();

        var result = _controller.Create(model).Result as CreatedAtRouteResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.Created);
    }

    [Fact]
    public void Create_CreateNotAllowed_ReturnsForbidden()
    {
        var model = new AnyModel();
        var controller = new AnyReadOnlyController(A.Fake<IRepository<string, AnyModel>>());

        var result = controller.Create(model).Result as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public void Create_NullModel_ReturnsBadRequest()
    {
        A.CallTo(() => _repository.Create(null))
            .Throws<ArgumentNullException>();

        var result = _controller.Create(null).Result as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public void Read_ExistingId_ReturnsModel()
    {
        const string id = "1";
        var model = new AnyModel();
        var expected = new Response<AnyModel>(model);
        
        A.CallTo(() => _repository.Get(id))
            .Returns(model);

        var actual = _controller.Read(id).Result as OkObjectResult;

        actual.Should().NotBeNull();
        actual!.Value.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Read_ExistingId_ReturnsOk()
    {
        const string id = "1";
        var model = new AnyModel();
        
        A.CallTo(() => _repository.Get(id))
            .Returns(model);

        var result = _controller.Read(id).Result as OkObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }
    
    [Fact]
    public void Read_NonExistingId_ReturnsNotFound()
    {
        const string id = "1";
        
        A.CallTo(() => _repository.Get(id))
            .Throws<NotFoundException>();

        var result = _controller.Read(id).Result as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }
    
    [Fact]
    public void Read_ReadNotAllowed_ReturnsForbidden()
    {
        var controller = new AnyCreateUpdateController(_repository);

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
        var expected = new Response<IEnumerable<AnyModel>>(models);
        
        A.CallTo(() => _repository.List())
            .Returns(models);

        var actual = _controller.ReadAll().Result as OkObjectResult;

        actual.Should().NotBeNull();
        actual!.Value.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void ReadAll_ExistingIds_ReturnsOk()
    {
        var models = new[]
        {
            new AnyModel(),
            new AnyModel()
        };
        
        A.CallTo(() => _repository.List())
            .Returns(models);

        var result = _controller.ReadAll().Result as OkObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }
    
    [Fact]
    public void ReadAll_ReadNotAllowed_ReturnsForbidden()
    {
        var controller = new AnyCreateUpdateController(_repository);

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

        A.CallTo(() => _repository.Update(id, model))
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
        var controller = new AnyReadOnlyController(A.Fake<IRepository<string, AnyModel>>());

        var result = controller.Update(id, model) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public void Update_NonExistingId_ReturnsNotFound()
    {
        const string id = "1";
        var model = new AnyModel();
        A.CallTo(() => _repository.Update(id, model))
            .Throws<NotFoundException>();

        var result = _controller.Update(id, model) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public void Update_NullArguments_ReturnsBadRequest()
    {
        A.CallTo(() => _repository.Update(null, null))
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

        A.CallTo(() => _repository.Delete(id))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Delete_DeleteForbidden_ReturnsForbidden()
    {
        const string id = "1";
        var controller = new AnyReadOnlyController(A.Fake<IRepository<string, AnyModel>>());

        var result = controller.Delete(id) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public void Delete_NonExistingId_ReturnsNotFound()
    {
        const string id = "1";
        A.CallTo(() => _repository.Delete(id))
            .Throws<NotFoundException>();

        var result = _controller.Delete(id) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public void Delete_NullId_ReturnsBadRequest()
    {
        const string id = "1";
        A.CallTo(() => _repository.Delete(id))
            .Throws<ArgumentNullException>();

        var result = _controller.Delete(id) as ObjectResult;

        result.Should().NotBeNull();
        result!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }
}