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
        public void Create_AnyEntity_EntityCreated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Create_AnyEntity_EntityCreated));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var expected = new AnyEntity
            {
                AnyString = "AnyString"
            };

            repository.Create(expected);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
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

            dbContextProvider.DbContext.Invoking(db => db.SaveChanges()).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void CreateRange_MultipleEntities_CreatesEntities()
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
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.First().AnyString.Should().Be(expected.First().AnyString);
            actual.Last().AnyString.Should().BeEquivalentTo(expected.Last().AnyString);
        }

        [Fact]
        public void Update_ExistingEntity_EntityUpdated()
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
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities.Single(e => e.Id == 1);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Update_NonExistingEntity_EntityNotUpdated()
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
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities.SingleOrDefault(e => e.Id == 2);
            actual.Should().BeNull();
        }

        [Fact]
        public void Update_NonExistingEntityWithCreate_EntityCreated()
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
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities.Single(e => e.Id == 2);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void UpdateRange_MultipleEntities_EntitiesUpdated()
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
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities.Where(e => new[] { 1, 2 }.Contains(e.Id));
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void UpdateRange_MultipleNonExistingEntities_EntitiesNotUpdated()
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
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities.Where(e => new[] { 3, 4 }.Contains(e.Id));
            actual.Should().BeEmpty();
        }

        [Fact]
        public void UpdateRange_ExistingAndNonExistingEntities_ExistingEntitiesUpdated()
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
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities.Where(e => new[] { 2, 3 }.Contains(e.Id));
            actual.Single().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void UpdateRange_MultipleEntitiesWithCreate_EntitiesUpdated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateRange_MultipleEntitiesWithCreate_EntitiesUpdated));
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

            repository.UpdateRange(expected, true);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities.Where(e => new[] { 1, 2 }.Contains(e.Id));
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void UpdateRange_MultipleNonExistingEntitiesWithCreate_EntitiesCreated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateRange_MultipleNonExistingEntitiesWithCreate_EntitiesCreated));
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
                    AnyString = "AnyString"
                },
                new AnyEntity
                {
                    Id = 4,
                    AnyString = "AnyString"
                }
            };
            var expected = new[]
            {
                dbContextProvider.DbContext.AnyEntities.Single(e => e.Id == 1),
                dbContextProvider.DbContext.AnyEntities.Single(e => e.Id == 2),
                nonExistingEntities.Single(e => e.Id == 3),
                nonExistingEntities.Single(e => e.Id == 4)
            };

            repository.UpdateRange(nonExistingEntities, true);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void UpdateRange_ExistingAndNonExistingEntitiesWithCreate_EntitiesCreated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateRange_ExistingAndNonExistingEntitiesWithCreate_EntitiesCreated));
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
                    Id = 2,
                    AnyString = "AnyNewString"
                },
                new AnyEntity
                {
                    Id = 3,
                    AnyString = "AnyNewString"
                }
            };
            var expected = new[]
            {
                dbContextProvider.DbContext.AnyEntities.Single(e => e.Id == 1),
                nonExistingEntities.Single(e => e.Id == 2),
                nonExistingEntities.Single(e => e.Id == 3)
            };

            repository.UpdateRange(nonExistingEntities, true);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Delete_ExistingEntity_EntityDeleted()
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
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Single().Should().BeEquivalentTo(entities.Last());
        }

        [Fact]
        public void DeleteRange_MultipleEntities_EntitiesDeleted()
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
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Single().Should().BeEquivalentTo(entities.Last());
        }
    }
}
