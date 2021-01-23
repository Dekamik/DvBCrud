using DvBCrud.EFCore.Tests.Mocks.DbContexts;
using DvBCrud.EFCore.Tests.Mocks.Entities;
using DvBCrud.EFCore.Tests.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
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
        public void GetRange_MultipleIds_ReturnsEntities()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(GetRange_MultipleIds_ReturnsEntities));
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
            dbContextProvider.Mock(new AnyEntity { Id = 3, AnyString = "Any" });

            var actual = repository.GetRange(new[] { 1, 2 });

            actual.Should().BeEquivalentTo(expected);
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
    }
}
