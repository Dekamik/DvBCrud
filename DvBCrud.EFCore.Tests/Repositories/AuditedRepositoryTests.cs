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
    public class AuditedRepositoryTests
    {
        private readonly ILogger logger;

        public AuditedRepositoryTests()
        {
            logger = A.Fake<ILogger>();
        }

        [Fact]
        public void Create_AnyAuditedEntity_EntityCreated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Create_AnyAuditedEntity_EntityCreated));
            var repository = new AnyAuditedRepository(dbContextProvider.DbContext, logger);
            var entityToCreate = new AnyAuditedEntity
            {
                AnyString = "AnyString"
            };
            var expected = new AnyAuditedEntity
            {
                AnyString = "AnyString",
                CreatedBy = 1
            };
            var expectedTime = DateTime.UtcNow;

            repository.Create(entityToCreate, 1);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyAuditedEntities.Single();
            actual.Should().BeEquivalentTo(expected, opts => opts.Excluding(x => x.Id).Excluding(x => x.CreatedAt));
            actual.CreatedAt.Should().BeCloseTo(expectedTime);
        }

        [Fact]
        public void CreateRange_MultipleEntities_EntitiesCreated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(CreateRange_MultipleEntities_EntitiesCreated));
            var repository = new AnyAuditedRepository(dbContextProvider.DbContext, logger);
            var entitiesToCreate = new[]
            {
                new AnyAuditedEntity
                {
                    AnyString = "AnyString"
                },
                new AnyAuditedEntity
                {
                    AnyString = "AnyString"
                }
            };
            var expectedEntities = new[]
            {
                new AnyAuditedEntity
                {
                    AnyString = "AnyString",
                    CreatedBy = 1
                },
                new AnyAuditedEntity
                {
                    AnyString = "AnyString",
                    CreatedBy = 1
                }
            };
            var expectedTime = DateTime.UtcNow;

            repository.CreateRange(entitiesToCreate, 1);
            dbContextProvider.DbContext.SaveChanges();

            var actualEntities = dbContextProvider.DbContext.AnyAuditedEntities.ToArray();
            for (int i = 0; i < dbContextProvider.DbContext.AnyAuditedEntities.Count(); i++)
            {
                var actual = actualEntities[i];
                var expected = expectedEntities[i];

                actual.Should().BeEquivalentTo(expected, opts => opts.Excluding(x => x.Id).Excluding(x => x.CreatedAt));
                actual.CreatedAt.Should().BeCloseTo(expectedTime);
            }
        }

        [Fact]
        public void Update_AnyAuditedEntity_EntityUpdated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Update_AnyAuditedEntity_EntityUpdated));
            var repository = new AnyAuditedRepository(dbContextProvider.DbContext, logger);
            var createdAt = DateTime.Parse($"{DateTime.Today.AddDays(-1):yyyy-MM-dd} 12:00:00");
            dbContextProvider.Mock(new[]
            {
                new AnyAuditedEntity
                {
                    Id = 1,
                    AnyString = "AnyString",
                    CreatedBy = 1,
                    CreatedAt = createdAt
                }
            });
            var expectedTime = DateTime.UtcNow;
            var expected = new AnyAuditedEntity
            {
                Id = 1,
                AnyString = "AnyNewString",
                CreatedBy = 1,
                CreatedAt = createdAt,
                UpdatedBy = 1
            };

            repository.Update(expected, 1);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyAuditedEntities.Single();
            actual.Should().BeEquivalentTo(expected, opts => opts.Excluding(x => x.UpdatedAt));
            actual.UpdatedAt.Should().BeCloseTo(expectedTime);
        }

        [Fact]
        public async Task UpdateAsync_AnyAuditedEntity_EntityUpdated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateAsync_AnyAuditedEntity_EntityUpdated));
            var repository = new AnyAuditedRepository(dbContextProvider.DbContext, logger);
            var createdAt = DateTime.Parse($"{DateTime.Today.AddDays(-1):yyyy-MM-dd} 12:00:00");
            dbContextProvider.Mock(new[]
            {
                new AnyAuditedEntity
                {
                    Id = 1,
                    AnyString = "AnyString",
                    CreatedBy = 1,
                    CreatedAt = createdAt
                }
            });
            var expectedTime = DateTime.UtcNow;
            var expected = new AnyAuditedEntity
            {
                Id = 1,
                AnyString = "AnyNewString",
                CreatedBy = 1,
                CreatedAt = createdAt,
                UpdatedBy = 1
            };

            await repository.UpdateAsync(expected, 1);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyAuditedEntities.Single();
            actual.Should().BeEquivalentTo(expected, opts => opts.Excluding(x => x.UpdatedAt));
            actual.UpdatedAt.Should().BeCloseTo(expectedTime);
        }

        [Fact]
        public void UpdateRange_AnyAuditedEntities_EntitiesUpdated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateRange_AnyAuditedEntities_EntitiesUpdated));
            var repository = new AnyAuditedRepository(dbContextProvider.DbContext, logger);
            var createdAt = DateTime.Parse($"{DateTime.Today.AddDays(-1):yyyy-MM-dd} 12:00:00");
            dbContextProvider.Mock(new[]
            {
                new AnyAuditedEntity
                {
                    Id = 1,
                    AnyString = "AnyString",
                    CreatedBy = 1,
                    CreatedAt = createdAt
                },
                new AnyAuditedEntity
                {
                    Id = 2,
                    AnyString = "AnyString",
                    CreatedBy = 1,
                    CreatedAt = createdAt
                }
            });
            var expectedEntities = new[]
            {
                new AnyAuditedEntity
                {
                    Id = 1,
                    AnyString = "AnyNewString",
                    CreatedBy = 1,
                    CreatedAt = createdAt,
                    UpdatedBy = 1
                },
                new AnyAuditedEntity
                {
                    Id = 2,
                    AnyString = "AnyNewString",
                    CreatedBy = 1,
                    CreatedAt = createdAt,
                    UpdatedBy = 1
                }
            };
            var expectedTime = DateTime.UtcNow;

            repository.UpdateRange(expectedEntities, 1);
            dbContextProvider.DbContext.SaveChanges();

            var actualEntities = dbContextProvider.DbContext.AnyAuditedEntities.ToArray();
            for (int i = 0; i < dbContextProvider.DbContext.AnyAuditedEntities.Count(); i++)
            {
                var actual = actualEntities[i];
                var expected = expectedEntities[i];

                actual.Should().BeEquivalentTo(expected, opts => opts.Excluding(x => x.Id).Excluding(x => x.UpdatedAt));
                actual.UpdatedAt.Should().BeCloseTo(expectedTime);
            }
        }
    }
}
