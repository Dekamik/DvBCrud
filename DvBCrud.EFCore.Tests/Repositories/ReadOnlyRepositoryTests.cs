using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using DvBCrud.EFCore.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DvBCrud.EFCore.Tests.Repositories
{
    public class ReadOnlyRepositoryTests
    {
        private readonly ILogger logger;
        private readonly AnyDbContext dbContext;
        private readonly IAnyReadOnlyRepository repository;

        public ReadOnlyRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AnyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            dbContext = new AnyDbContext(options);
            logger = A.Fake<ILogger>();
            repository = new AnyReadOnlyRepository(dbContext, logger);
        }

        [Fact]
        public void GetAll_Default_ReturnsAll()
        {
            // Arrange
            var expected = new[]
            {
                new AnyEntity
                {
                    Id = 1,
                    AnyString = "Any"
                },
                new AnyEntity
                {
                    Id = 2,
                    AnyString = "Any"
                }
            };
            dbContext.AnyEntities.AddRange(expected);
            dbContext.SaveChanges();

            // Act
            var actual = repository.GetAll();

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
                    Id = 1,
                    AnyString = "Any"
                },
                new AnyEntity {
                    Id = 2,
                    AnyString = "Any"
                }
            };
            dbContext.AnyEntities.AddRange(expected);
            dbContext.SaveChanges();

            // Act
            var actual = repository.Get(1);

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
                    Id = 1,
                    AnyString = "Any"
                },
                new AnyEntity {
                    Id = 2,
                    AnyString = "Any"
                }
            };
            dbContext.AnyEntities.AddRange(expected);
            dbContext.SaveChanges();

            // Act
            var actual = repository.Get(3);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void Get_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var nullableRepository = new AnyNullableIdRepository(dbContext, logger);

            // Act & Assert
            nullableRepository.Invoking(r => r.Get(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task GetAsync_ExistingId_ReturnsEntity()
        {
            // Arrange
            var expected = new[]
            {
                new AnyEntity
                {
                    Id = 1,
                    AnyString = "Any"
                },
                new AnyEntity {
                    Id = 2,
                    AnyString = "Any"
                }
            };
            dbContext.AnyEntities.AddRange(expected);
            dbContext.SaveChanges();

            // Act
            var actual = await repository.GetAsync(1);

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
                    Id = 1,
                    AnyString = "Any"
                },
                new AnyEntity {
                    Id = 2,
                    AnyString = "Any"
                }
            };
            dbContext.AnyEntities.AddRange(expected);
            dbContext.SaveChanges();

            // Act
            var actual = await repository.GetAsync(3);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var repository = new AnyNullableIdRepository(dbContext, logger);

            // Act & Assert
            await repository.Awaiting(r => r.GetAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }
    }
}
