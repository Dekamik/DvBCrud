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
        public async Task Create_AnyEntity_EntityCreated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Create_AnyEntity_EntityCreated));
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
        public async Task CreateRange_MultipleEntities_CreatesEntities()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(CreateRange_MultipleEntities_CreatesEntities));
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
        public async Task Update_ExistingEntity_EntityUpdated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Update_ExistingEntity_EntityUpdated));
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
            await repository.SaveChanges();
            var actual = repository.Get(1);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task Update_NonExistingEntity_EntityNotUpdated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Update_NonExistingEntity_EntityNotUpdated));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            dbContextProvider.Mock(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            var updatedEntity = new AnyEntity
            {
                Id = 2,
                AnyString = "AnyNewString"
            };

            repository.Update(updatedEntity);
            await repository.SaveChanges();
            var actual = repository.Get(2);

            actual.Should().BeNull();
        }

        [Fact]
        public async Task Update_NonExistingEntityWithCreate_EntityCreated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Update_NonExistingEntity_EntityNotUpdated));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            dbContextProvider.Mock(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            var expected = new AnyEntity
            {
                Id = 2,
                AnyString = "AnyNewString"
            };

            repository.Update(expected, true);
            await repository.SaveChanges();
            var actual = repository.Get(2);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateRange_MultipleEntities_EntitiesUpdated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateRange_MultipleEntities_EntitiesUpdated));
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
            await repository.SaveChanges();
            var actual = repository.GetRange(new[] { 1, 2 });

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateRange_MultipleNonExistingEntities_EntitiesNotUpdated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateRange_MultipleNonExistingEntities_EntitiesNotUpdated));
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
            var nonExistingEntities = new[] {
                new AnyEntity
                {
                    Id = 3,
                    AnyString = "AnyNewString"
                },
                new AnyEntity
                {
                    Id = 4,
                    AnyString = "AnyNewString"
                }
            };

            repository.UpdateRange(nonExistingEntities);
            await repository.SaveChanges();
            var actual = repository.GetRange(new[] { 3, 4 });

            actual.Should().BeEmpty();
        }

        [Fact]
        public async Task UpdateRange_ExistingAndNonExistingEntities_ExistingEntitiesUpdated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateRange_ExistingAndNonExistingEntities_ExistingEntitiesUpdated));
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
            var nonExistingEntities = new[] {
                new AnyEntity
                {
                    Id = 3,
                    AnyString = "AnyNewString"
                },
                new AnyEntity
                {
                    Id = 4,
                    AnyString = "AnyNewString"
                }
            };
            var expected = dbContextProvider.DbContext.AnyEntities.Single(e => e.Id == 2);
            var selection = new[]
            {
                expected,
                nonExistingEntities.Single(e => e.Id == 3)
            };

            repository.UpdateRange(nonExistingEntities);
            await repository.SaveChanges();
            var actual = dbContextProvider.DbContext.AnyEntities.Where(e => new[] { 2, 3 }.Contains(e.Id)); // Get AnyEntity 2 and 3

            actual.Single().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task Delete_ExistingEntity_EntityDeleted()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Delete_ExistingEntity_EntityDeleted));
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
        public async Task DeleteRange_MultipleEntities_EntitiesDeleted()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(DeleteRange_MultipleEntities_EntitiesDeleted));
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
