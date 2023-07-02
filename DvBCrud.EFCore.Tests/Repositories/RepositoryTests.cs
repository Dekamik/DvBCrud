using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DvBCrud.EFCore.Mocks.Core.DbContexts;
using DvBCrud.EFCore.Mocks.Core.Entities;
using DvBCrud.EFCore.Mocks.Core.Repositories;
using DvBCrud.EFCore.Mocks.Services.Model;
using Xunit;

namespace DvBCrud.EFCore.Tests.Repositories
{
    public class RepositoryTests
    {
        private readonly AnyDbContext _dbContext;
        private readonly AnyMapper _mapper;
        private readonly IAnyRepository _repository;

        public RepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AnyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _dbContext = new AnyDbContext(options);
            _mapper = new AnyMapper();
            _repository = new AnyRepository(_dbContext, _mapper);
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
                    AnyString = "Any"
                },
                new AnyEntity
                {
                    Id = "2",
                    AnyString = "Any"
                }
            };
            _dbContext.AnyEntities.AddRange(expected);
            _dbContext.SaveChanges();

            // Act
            var actual = _repository.List();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Get_ExistingId_ReturnsEntity()
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
            _dbContext.AnyEntities.AddRange(expected);
            _dbContext.SaveChanges();

            // Act
            var actual = _repository.Get("1");

            // Assert
            actual.Should().BeEquivalentTo(expected.First());
        }

        [Fact]
        public void Get_NonExistingId_ReturnsNull()
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
            _dbContext.AnyEntities.AddRange(expected);
            _dbContext.SaveChanges();

            // Act
            var actual = _repository.Get("3");

            // Assert
            actual.Should().BeNull();
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
                    AnyString = "Any"
                },
                new AnyEntity {
                    Id = "2",
                    AnyString = "Any"
                }
            };
            _dbContext.AnyEntities.AddRange(expected);
            _dbContext.SaveChanges();

            // Act
            var actual = await _repository.GetAsync("1");

            // Assert
            actual.Should().BeEquivalentTo(expected.First());
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
            _dbContext.AnyEntities.AddRange(expected);
            _dbContext.SaveChanges();

            // Act
            var actual = await _repository.GetAsync("3");

            // Assert
            actual.Should().BeNull();
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
            var expected = new AnyEntity
            {
                AnyString = "AnyString"
            };

            // Act
            _repository.Create(expected);
            _dbContext.SaveChanges();

            // Assert
            _dbContext.AnyEntities.First().AnyString.Should().Be(expected.AnyString);
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
            _dbContext.AnyEntities.Add(entity);
            _dbContext.SaveChanges();

            // Act
            _repository.Invoking(x => x.Create(entity)).Should().Throw<ArgumentException>();
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
            var expected = new AnyEntity
            {
                AnyString = "AnyString"
            };

            // Act
            await _repository.CreateAsync(expected);
            await _dbContext.SaveChangesAsync();

            // Assert
            _dbContext.AnyEntities.First().AnyString.Should().Be(expected.AnyString);
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
            _dbContext.AnyEntities.Add(entity);
            _dbContext.SaveChanges();

            // Act
            _repository.Invoking(x => x.CreateAsync(entity)).Should().ThrowAsync<ArgumentException>();
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
            _dbContext.AnyEntities.Add(new AnyEntity
            {
                Id = "1",
                AnyString = "AnyString"
            });
            _dbContext.SaveChanges();
            var expected = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            // Act
            _repository.Update("1", expected);

            // Assert
            _dbContext.AnyEntities.First(e => e.Id == "1")
                .AnyString
                .Should()
                .BeEquivalentTo(expected.AnyString);
        }

        [Fact]
        public void Update_NonExistingEntity_ThrowsKeyNotFoundException()
        {
            // Arrange
            _dbContext.AnyEntities.Add(new AnyEntity
            {
                Id = "1",
                AnyString = "AnyString"
            });
            _dbContext.SaveChanges();
            var updatedEntity = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            // Act & Assert
            _repository.Invoking(r => r.Update("2", updatedEntity))
                .Should()
                .Throw<KeyNotFoundException>();
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
            var updatedEntity = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            // Act & Assert
            _repository.Invoking(r => r.Update(null, updatedEntity)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateAsync_ExistingEntity_EntityUpdatedAsync()
        {
            // Arrange
            _dbContext.AnyEntities.Add(new AnyEntity
            {
                Id = "1",
                AnyString = "AnyString"
            });
            _dbContext.SaveChanges();
            var expected = new AnyEntity
            {
                Id = "1",
                AnyString = "AnyNewString"
            };

            // Act
            await _repository.UpdateAsync("1", expected);

            // Assert
            _dbContext.AnyEntities.First(e => e.Id == "1").AnyString
                .Should()
                .Be(expected.AnyString);
        }

        [Fact]
        public async Task UpdateAsync_NonExistingEntity_ThrowsKeyNotFoundException()
        {
            // Arrange
            _dbContext.AnyEntities.Add(new AnyEntity
            {
                Id = "1",
                AnyString = "AnyString"
            });
            _dbContext.SaveChanges();
            var updatedEntity = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            // Act & Assert
            await _repository.Awaiting(r => r.UpdateAsync("2", updatedEntity))
                .Should()
                .ThrowAsync<KeyNotFoundException>();
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
            var updatedEntity = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            // Act & Assert
            await _repository.Awaiting(r => r.UpdateAsync(null, updatedEntity))
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
            _dbContext.AnyEntities.AddRange(entities);
            _dbContext.SaveChanges();
            _dbContext.AnyEntities.Should().Contain(entities);

            // Act
            _repository.Delete("1");
            _dbContext.SaveChanges();

            // Assert
            _dbContext.AnyEntities.First()
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
                .Throw<KeyNotFoundException>();

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
            _dbContext.AnyEntities.AddRange(entities);
            _dbContext.SaveChanges();
            _dbContext.AnyEntities.Should().Contain(entities);

            // Act
            await _repository.DeleteAsync("1");
            _dbContext.SaveChanges();

            // Assert
            _dbContext.AnyEntities.First().Should().BeEquivalentTo(entities.Last());
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
                .ThrowAsync<KeyNotFoundException>();


        [Fact]
        public void Exists_EntityExists_ReturnsTrue()
        {
            var entity = new AnyEntity
            {
                Id = "1",
                AnyString = "AnyString"
            };
            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            _repository.Exists("1").Should().BeTrue();
        }

        [Fact]
        public void Exists_EntityDoesntExist_ReturnsFalse()
        {
            _repository.Exists("1").Should().BeFalse();
        }
    }
}
