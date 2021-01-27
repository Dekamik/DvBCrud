using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
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

        public ReadOnlyRepositoryTests()
        {
            logger = A.Fake<ILogger>();
        }

        [Fact]
        public void GetAll_Default_ReturnsAll()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(GetAll_Default_ReturnsAll));
            var repository = new AnyReadOnlyRepository(dbContextProvider.DbContext, logger);
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
            dbContextProvider.Mock(expected);

            var actual = repository.GetAll();

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Get_ExistingId_ReturnsEntity()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Get_ExistingId_ReturnsEntity));
            var repository = new AnyReadOnlyRepository(dbContextProvider.DbContext, logger);
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
            dbContextProvider.Mock(expected);

            var actual = repository.Get(1);

            actual.Should().BeEquivalentTo(expected.First());
        }

        [Fact]
        public void Get_NonExistingId_ReturnsNull()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Get_NonExistingId_ReturnsNull));
            var repository = new AnyReadOnlyRepository(dbContextProvider.DbContext, logger);
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
            dbContextProvider.Mock(expected);

            var actual = repository.Get(3);

            actual.Should().BeNull();
        }

        [Fact]
        public void Get_Null_ThrowsArgumentNullException()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Get_Null_ThrowsArgumentNullException));
            var repository = new AnyNullableIdRepository(dbContextProvider.DbContext, logger);

            repository.Invoking(r => r.Get(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task GetAsync_ExistingId_ReturnsEntity()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(GetAsync_ExistingId_ReturnsEntity));
            var repository = new AnyReadOnlyRepository(dbContextProvider.DbContext, logger);
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
            dbContextProvider.Mock(expected);

            var actual = await repository.GetAsync(1);

            actual.Should().BeEquivalentTo(expected.First());
        }

        [Fact]
        public async Task GetAsync_NonExistingId_ReturnsNull()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(GetAsync_NonExistingId_ReturnsNull));
            var repository = new AnyReadOnlyRepository(dbContextProvider.DbContext, logger);
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
            dbContextProvider.Mock(expected);

            var actual = await repository.GetAsync(3);

            actual.Should().BeNull();
        }

        [Fact]
        public void GetAsync_Null_ThrowsArgumentNullException()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(GetAsync_Null_ThrowsArgumentNullException));
            var repository = new AnyNullableIdRepository(dbContextProvider.DbContext, logger);

            repository.Invoking(r => r.GetAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }
    }
}
