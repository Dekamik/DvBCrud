using DvBCrud.EFCore.Tests.Mocks.DbContexts;
using DvBCrud.EFCore.Tests.Mocks.Entities;
using DvBCrud.EFCore.Tests.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DvBCrud.EFCore.Tests.Repositories
{
    public class RepositoryTests
    {
        private readonly ILogger logger;

        public RepositoryTests()
        {
            logger = A.Fake<ILogger>();
        }

        [Fact]
        public async Task Create_AnyEntity_EntityCreatedAsync()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Create_AnyEntity_EntityCreatedAsync));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var expected = new AnyEntity
            {
                AnyString = "AnyString"
            };

            repository.Create(expected);
            await repository.SaveChanges();

            var actual = repository.GetAll();

            actual.Single().AnyString.Should().Be(expected.AnyString);
        }

        [Fact]
        public void Create_ExistingEntity_ThrowsArgumentException()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Create_ExistingEntity_ThrowsArgumentException));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            dbContextProvider.Mock(entity);

            repository.Create(entity);

            repository.Invoking(r => r.SaveChanges()).Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateRange_MultipleEntities_CreatesEntitiesAsync()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(CreateRange_MultipleEntities_CreatesEntitiesAsync));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var expected = new[]
            {
                new AnyEntity
                {
                    AnyString = "FirstEntity"
                },
                new AnyEntity
                {
                    AnyString = "SecondEntity"
                }
            };

            repository.CreateRange(expected);
            await repository.SaveChanges();
            var actual = repository.GetAll();

            actual.First().AnyString.Should().Be(expected.First().AnyString);
            actual.Last().AnyString.Should().BeEquivalentTo(expected.Last().AnyString);
        }

        [Fact]
        public async Task Update_ExistingEntity_EntityUpdatedAsync()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Update_ExistingEntity_EntityUpdatedAsync));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            dbContextProvider.Mock(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            var expected = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            repository.Update(expected);
            var actual = repository.Get(1);
            await repository.SaveChanges();

            actual.Single().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateRange_MultipleEntities_EntitiesUpdatedAsync()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateRange_MultipleEntities_EntitiesUpdatedAsync));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            dbContextProvider.Mock(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            },
            new AnyEntity
            {
                Id = 2,
                AnyString = "AnyString"
            });
            var expected = new[] {
                new AnyEntity
                {
                    Id = 1,
                    AnyString = "AnyNewString"
                },
                new AnyEntity
                {
                    Id = 2,
                    AnyString = "AnyNewString"
                }
            };

            repository.UpdateRange(expected);
            var actual = repository.GetRange(new[] { 1, 2 });
            await repository.SaveChanges();

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task Delete_ExistingEntity_EntityDeletedAsync()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Delete_ExistingEntity_EntityDeletedAsync));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var entities = new[] {
                new AnyEntity
                {
                    Id = 1,
                    AnyString = "AnyString"
                },
                new AnyEntity
                {
                    Id = 2,
                    AnyString = "AnyString"
                }
            };
            dbContextProvider.Mock(entities);

            repository.Delete(1);
            await repository.SaveChanges();
            var actual = repository.GetAll();

            actual.Single().Should().BeEquivalentTo(entities.Last());
        }

        [Fact]
        public async Task DeleteRange_MultipleEntities_EntitiesDeletedAsync()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(DeleteRange_MultipleEntities_EntitiesDeletedAsync));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var entities = new[] {
                new AnyEntity
                {
                    Id = 1,
                    AnyString = "AnyString"
                },
                new AnyEntity
                {
                    Id = 2,
                    AnyString = "AnyString"
                },
                new AnyEntity
                {
                    Id = 3,
                    AnyString = "AnyString"
                }
            };
            dbContextProvider.Mock(entities);

            repository.DeleteRange(new[] { 1, 2 });
            await repository.SaveChanges();
            var actual = repository.GetAll();

            actual.Single().Should().BeEquivalentTo(entities.Last());
        }
    }
}
