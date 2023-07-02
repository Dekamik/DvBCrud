using System;
using System.Linq;
using System.Threading.Tasks;
using DvBCrud.EFCore.Mocks.Core.Entities;
using DvBCrud.EFCore.Mocks.Core.Repositories;
using DvBCrud.EFCore.Mocks.Services;
using DvBCrud.EFCore.Mocks.Services.Model;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace DvBCrud.EFCore.Services.Tests;

public class ServiceTests
{
    private readonly IAnyMapper _mapper;
    private readonly IAnyRepository _repository;
    private readonly AnyService _service;

    public ServiceTests()
    {
        _mapper = A.Fake<IAnyMapper>();
        _repository = A.Fake<IAnyRepository>();
        _service = new AnyService(_repository, _mapper);
    }

    [Fact]
    public void GetAll_Any_ReturnsAllEntities()
    {
        var entities = new[]
        {
            new AnyEntity
            {
                Id = "1",
                AnyString = "Any1"
            },
            new AnyEntity
            {
                Id = "2",
                AnyString = "Any2"
            }
        }.AsQueryable();

        var models = new[]
        {
            new AnyModel
            {
                Id = "1",
                AnyString = "Any1"
            },
            new AnyModel
            {
                Id = "2",
                AnyString = "Any2"
            }
        };

        A.CallTo(() => _repository.List())
            .Returns(entities);
        A.CallTo(() => _mapper.ToModel(entities.First()))
            .Returns(models.First());
        A.CallTo(() => _mapper.ToModel(entities.Last()))
            .Returns(models.Last());

        var actual = _service.GetAll();

        actual.Should().Contain(models);
    }

    [Fact]
    public void Get_AnyId_ReturnsEntity()
    {
        const string id = "1";
        var entity = new AnyEntity();
        var expected = new AnyModel();

        A.CallTo(() => _repository.Get(id))
            .Returns(entity);
        A.CallTo(() => _mapper.ToModel(entity))
            .Returns(expected);

        var actual = _service.Get(id);

        actual.Should().Be(expected);
    }

    [Fact]
    public async Task GetAsync_AnyId_ReturnsEntity()
    {
        const string id = "1";
        var entity = new AnyEntity();
        var expected = new AnyModel();
        
        A.CallTo(() => _repository.GetAsync(id))
            .Returns(Task.FromResult((AnyEntity?)entity));
        A.CallTo(() => _mapper.ToModel(entity))
            .Returns(expected);
        
        var actual = await _service.GetAsync(id);

        actual.Should().Be(expected);
    }

    [Fact]
    public void Create_AnyModel_CreatesEntityInRepository()
    {
        var model = new AnyModel();
        var entity = new AnyEntity();

        A.CallTo(() => _mapper.ToEntity(model))
            .Returns(entity);
        
        _service.Create(model);

        A.CallTo(() => _repository.Create(entity))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Create_NullModel_ThrowsArgumentNullException() =>
        _service.Invoking(x => x.Create(null))
            .Should()
            .Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'model')");
    
    [Fact]
    public async Task CreateAsync_AnyModel_CreatesEntityInRepository()
    {
        var model = new AnyModel();
        var entity = new AnyEntity();

        A.CallTo(() => _mapper.ToEntity(model))
            .Returns(entity);
        
        await _service.CreateAsync(model);

        A.CallTo(() => _repository.CreateAsync(entity))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task CreateAsync_NullModel_ThrowsArgumentNullException() =>
        await _service.Awaiting(x => x.CreateAsync(null))
            .Should()
            .ThrowAsync<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'model')");

    [Fact]
    public void Update_Any_UpdatesEntityInRepository()
    {
        const string id = "1";
        var model = new AnyModel();
        var entity = new AnyEntity();

        A.CallTo(() => _mapper.ToEntity(model))
            .Returns(entity);
        
        _service.Update(id, model);

        A.CallTo(() => _repository.Update(id, A<AnyEntity>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Update_NullId_ThrowsArgumentNullException() =>
        _service.Invoking(x => x.Update(null, null))
            .Should()
            .Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'id')");

    [Fact]
    public void Update_NullModel_ThrowsArgumentNullException() =>
        _service.Invoking(x => x.Update("1", null))
            .Should()
            .Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'model')");
    
    [Fact]
    public async Task UpdateAsync_Any_UpdatesEntityInRepository()
    {
        const string id = "1";
        var model = new AnyModel();
        var entity = new AnyEntity();

        A.CallTo(() => _mapper.ToEntity(model))
            .Returns(entity);
        
        await _service.UpdateAsync(id, model);

        A.CallTo(() => _repository.UpdateAsync(id, entity))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task UpdateAsync_NullId_ThrowsArgumentNullException() =>
        await _service.Awaiting(x => x.UpdateAsync(null, null))
            .Should()
            .ThrowAsync<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'id')");

    [Fact]
    public async Task UpdateAsync_NullModel_ThrowsArgumentNullException() =>
        await _service.Awaiting(x => x.UpdateAsync("1", null))
            .Should()
            .ThrowAsync<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'model')");

    [Fact]
    public void Delete_Any_DeletesInRepository()
    {
        const string id = "1";
        
        _service.Delete(id);

        A.CallTo(() => _repository.Delete(id))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Delete_NullId_ThrowsArgumentNullException() =>
        _service.Invoking(x => x.Delete(null))
            .Should()
            .Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'id')");
    
    [Fact]
    public async Task DeleteAsync_Any_DeletesInRepository()
    {
        const string id = "1";
        
        await _service.DeleteAsync(id);

        A.CallTo(() => _repository.DeleteAsync(id))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteAsync_NullId_ThrowsArgumentNullException() =>
        await _service.Awaiting(x => x.DeleteAsync(null))
            .Should()
            .ThrowAsync<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'id')");
}