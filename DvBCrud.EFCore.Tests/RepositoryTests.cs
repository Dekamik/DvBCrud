using System;
using System.Linq;
using System.Threading.Tasks;
using DvBCrud.EFCore.Tests.Mocks;
using DvBCrud.EFCore.Tests.Mocks.Mappers;
using DvBCrud.EFCore.Tests.Mocks.Repositories;
using DvBCrud.Shared.Exceptions;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

#pragma warning disable CS8625

namespace DvBCrud.EFCore.Tests;

public class RepositoryTests : IClassFixture<AnyDbContextFixture>
{
    private readonly AnyDbContextFixture _dbContextFixture;
    private readonly IAnyRepository _repository;

    public RepositoryTests(AnyDbContextFixture dbContextFixture)
    {
        _dbContextFixture = dbContextFixture;
        var mapper = new AnyMapper();
        _repository = new AnyRepository(_dbContextFixture.DbContext, mapper);
    }

    [Fact]
    public void GetAll_Default_ReturnsAll()
    {
        // Arrange
        var expected = new[]
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
        };
        _dbContextFixture.DbContext.AnyEntities.AddRange(expected);
        _dbContextFixture.DbContext.SaveChanges();

        // Act
        var actual = _repository.List();

        // Assert
        actual.First().AnyString.Should().BeEquivalentTo(expected.First().AnyString);
    }

    [Fact]
    public void Get_ExistingId_ReturnsEntity()
    {
        // Arrange
        var utcNow = DateTimeOffset.UtcNow;
        var entities = new[]
        {
            new AnyEntity
            {
                Id = "1",
                AnyString = "AnyOne"
            },
            new AnyEntity {
                Id = "2",
                AnyString = "AnyTwo"
            }
        };
        _dbContextFixture.DbContext.AnyEntities.AddRange(entities);
        _dbContextFixture.DbContext.SaveChanges();

        // Act
        var model = _repository.Get("1");

        // Assert
        model!.AnyString.Should().BeEquivalentTo(entities.First().AnyString);
    }

    [Fact]
    public void Get_NonExistingId_ThrowsNotFound()
    {
        // Arrange
        var expected = new[]
        {
            new AnyEntity
            {
                Id = "1",
                AnyString = "Any"
            },
            new AnyEntity {
                Id = "2",
                AnyString = "Any"
            }
        };
        _dbContextFixture.DbContext.AnyEntities.AddRange(expected);
        _dbContextFixture.DbContext.SaveChanges();

        // Act
        _repository.Invoking(r => r.Get("3"))
            .Should()
            .Throw<NotFoundException>();
    }

    [Fact]
    public void Get_Null_ThrowsArgumentNullException() => 
        _repository.Invoking(r => r.Get(null))
            .Should()
            .Throw<ArgumentNullException>();

    [Fact]
    public async Task GetAsync_ExistingId_ReturnsEntity()
    {
        // Arrange
        var expected = new[]
        {
            new AnyEntity
            {
                Id = "1",
                AnyString = "Any1"
            },
            new AnyEntity {
                Id = "2",
                AnyString = "Any2"
            }
        };
        _dbContextFixture.DbContext.AnyEntities.AddRange(expected);
        await _dbContextFixture.DbContext.SaveChangesAsync();

        // Act
        var actual = await _repository.GetAsync("1");

        // Assert
        actual!.AnyString.Should().BeEquivalentTo(expected.First().AnyString);
    }

    [Fact]
    public async Task GetAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        var expected = new[]
        {
            new AnyEntity
            {
                Id = "1",
                AnyString = "Any"
            },
            new AnyEntity {
                Id = "2",
                AnyString = "Any"
            }
        };
        _dbContextFixture.DbContext.AnyEntities.AddRange(expected);
        await _dbContextFixture.DbContext.SaveChangesAsync();

        // Act
        await _repository.Awaiting(r => r.GetAsync("3"))
            .Should()
            .ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task GetAsync_Null_ThrowsArgumentNullException() => 
        await _repository.Awaiting(r => r.GetAsync(null))
            .Should()
            .ThrowAsync<ArgumentNullException>();

    [Fact]
    public void Create_AnyEntity_EntityCreated()
    {
        // Arrange
        var expected = new AnyModel
        {
            AnyString = "AnyString"
        };

        // Act
        _repository.Create(expected);
        _dbContextFixture.DbContext.SaveChanges();

        // Assert
        _dbContextFixture.DbContext.AnyEntities.First().AnyString.Should().Be(expected.AnyString);
    }

    [Fact]
    public void Create_ExistingEntity_ThrowsArgumentException()
    {
        // Arrange
        var entity = new AnyEntity
        {
            Id = "1",
            AnyString = "AnyString"
        };
        var model = new AnyModel
        {
            Id = "1",
            AnyString = "AnyString"
        };
        _dbContextFixture.DbContext.AnyEntities.Add(entity);
        _dbContextFixture.DbContext.SaveChanges();

        // Act
        _repository.Invoking(x => x.Create(model)).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Create_Null_ThrowsArgumentNullException() => 
        _repository.Invoking(r => r.Create(null))
            .Should()
            .Throw<ArgumentNullException>();
        
    [Fact]
    public async Task CreateAsync_AnyEntity_EntityCreated()
    {
        // Arrange
        var expected = new AnyModel
        {
            AnyString = "AnyString"
        };

        // Act
        await _repository.CreateAsync(expected);
        await _dbContextFixture.DbContext.SaveChangesAsync();

        // Assert
        _dbContextFixture.DbContext.AnyEntities.First().AnyString.Should().Be(expected.AnyString);
    }

    [Fact]
    public void CreateAsync_ExistingEntity_ThrowsArgumentException()
    {
        // Arrange
        var entity = new AnyEntity
        {
            Id = "1",
            AnyString = "AnyString"
        };
        var model = new AnyModel
        {
            Id = "1",
            AnyString = "AnyString"
        };
        _dbContextFixture.DbContext.AnyEntities.Add(entity);
        _dbContextFixture.DbContext.SaveChanges();

        // Act
        _repository.Invoking(x => x.CreateAsync(model)).Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public void CreateAsync_Null_ThrowsArgumentNullException() => 
        _repository.Invoking(r => r.CreateAsync(null))
            .Should()
            .ThrowAsync<ArgumentNullException>();

    [Fact]
    public void Update_ExistingEntity_EntityUpdated()
    {
        // Arrange
        _dbContextFixture.DbContext.AnyEntities.Add(new AnyEntity
        {
            Id = "1",
            AnyString = "AnyString"
        });
        _dbContextFixture.DbContext.SaveChanges();
        var expected = new AnyModel
        {
            AnyString = "AnyNewString"
        };

        // Act
        _repository.Update("1", expected);

        // Assert
        _dbContextFixture.DbContext.AnyEntities.First(e => e.Id == "1")
            .AnyString
            .Should()
            .BeEquivalentTo(expected.AnyString);
    }

    [Fact]
    public void Update_NonExistingEntity_ThrowsKeyNotFoundException()
    {
        // Arrange
        _dbContextFixture.DbContext.AnyEntities.Add(new AnyEntity
        {
            Id = "1",
            AnyString = "AnyString"
        });
        _dbContextFixture.DbContext.SaveChanges();
        var updatedModel = new AnyModel
        {
            AnyString = "AnyNewString"
        };

        // Act & Assert
        _repository.Invoking(r => r.Update("2", updatedModel))
            .Should()
            .Throw<NotFoundException>();
    }

    [Fact]
    public void Update_Null_ThrowsArgumentNullException()
    {
        _repository.Invoking(r => r.Update("1", null))
            .Should()
            .Throw<ArgumentNullException>();
    }

    [Fact]
    public void Update_NullId_ThrowsArgumentNullException()
    {
        // Arrange
        var updatedModel = new AnyModel
        {
            AnyString = "AnyNewString"
        };

        // Act & Assert
        _repository.Invoking(r => r.Update(null, updatedModel)).Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateAsync_ExistingEntity_EntityUpdatedAsync()
    {
        // Arrange
        _dbContextFixture.DbContext.AnyEntities.Add(new AnyEntity
        {
            Id = "1",
            AnyString = "AnyString"
        });
        await _dbContextFixture.DbContext.SaveChangesAsync();
        var expected = new AnyModel
        {
            Id = "1",
            AnyString = "AnyNewString"
        };

        // Act
        await _repository.UpdateAsync("1", expected);

        // Assert
        _dbContextFixture.DbContext.AnyEntities.First(e => e.Id == "1").AnyString
            .Should()
            .Be(expected.AnyString);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingEntity_ThrowsKeyNotFoundException()
    {
        // Arrange
        _dbContextFixture.DbContext.AnyEntities.Add(new AnyEntity
        {
            Id = "1",
            AnyString = "AnyString"
        });
        await _dbContextFixture.DbContext.SaveChangesAsync();
        var updatedModel = new AnyModel
        {
            AnyString = "AnyNewString"
        };

        // Act & Assert
        await _repository.Awaiting(r => r.UpdateAsync("2", updatedModel))
            .Should()
            .ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task UpdateAsync_Null_ThrowsArgumentNullException() => 
        await _repository.Awaiting(r => r.UpdateAsync("1", null))
            .Should()
            .ThrowAsync<ArgumentNullException>();

    [Fact]
    public async Task UpdateAsync_NullId_ThrowsArgumentNullException()
    {
        // Arrange
        var updatedModel = new AnyModel
        {
            AnyString = "AnyNewString"
        };

        // Act & Assert
        await _repository.Awaiting(r => r.UpdateAsync(null, updatedModel))
            .Should()
            .ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public void Delete_ExistingEntity_EntityDeleted()
    {
        // Arrange
        var entities = new[] {
            new AnyEntity
            {
                Id = "1",
                AnyString = "AnyString"
            },
            new AnyEntity
            {
                Id = "2",
                AnyString = "AnyString"
            }
        };
        _dbContextFixture.DbContext.AnyEntities.AddRange(entities);
        _dbContextFixture.DbContext.SaveChanges();
        _dbContextFixture.DbContext.AnyEntities.Should().Contain(entities);

        // Act
        _repository.Delete("1");
        _dbContextFixture.DbContext.SaveChanges();

        // Assert
        _dbContextFixture.DbContext.AnyEntities.First()
            .Should()
            .BeEquivalentTo(entities.Last());
    }

    [Fact]
    public void Delete_Null_ThrowsArgumentNullException() =>
        _repository.Invoking(r => r.Delete(null))
            .Should()
            .Throw<ArgumentNullException>();

    [Fact]
    public void Delete_NonExistingId_ThrowsKeyNotFoundException() => 
        _repository.Invoking(r => r.Delete("1"))
            .Should()
            .Throw<NotFoundException>();

    [Fact]
    public async Task DeleteAsync_ExistingEntity_EntityDeleted()
    {
        // Arrange
        var entities = new[] {
            new AnyEntity
            {
                Id = "1",
                AnyString = "AnyString"
            },
            new AnyEntity
            {
                Id = "2",
                AnyString = "AnyString"
            }
        };
        _dbContextFixture.DbContext.AnyEntities.AddRange(entities);
        await _dbContextFixture.DbContext.SaveChangesAsync();
        _dbContextFixture.DbContext.AnyEntities.Should().Contain(entities);

        // Act
        await _repository.DeleteAsync("1");
        await _dbContextFixture.DbContext.SaveChangesAsync();

        // Assert
        _dbContextFixture.DbContext.AnyEntities.First().Should().BeEquivalentTo(entities.Last());
    }

    [Fact]
    public async Task DeleteAsync_Null_ThrowsArgumentNullException() =>
        await _repository.Awaiting(r => r.DeleteAsync(null))
            .Should()
            .ThrowAsync<ArgumentNullException>();

    [Fact]
    public async Task DeleteAsync_NonExistingId_ThrowsKeyNotFoundException() => 
        await _repository.Awaiting(r => r.DeleteAsync("1"))
            .Should()
            .ThrowAsync<NotFoundException>();


    [Fact]
    public void Exists_EntityExists_ReturnsTrue()
    {
        var entity = new AnyEntity
        {
            Id = "1",
            AnyString = "AnyString"
        };
        _dbContextFixture.DbContext.Add(entity);
        _dbContextFixture.DbContext.SaveChanges();

        _repository.Exists("1").Should().BeTrue();
    }

    [Fact]
    public void Exists_EntityDoesntExist_ReturnsFalse()
    {
        _repository.Exists("1").Should().BeFalse();
    }
}