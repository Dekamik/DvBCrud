using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
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
    public class AuditedRepositoryTests
    {
        private readonly ILogger logger;
        private readonly AnyDbContext dbContext;
        private readonly IAnyAuditedRepository repository;

        public AuditedRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AnyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            dbContext = new AnyDbContext(options);
            logger = A.Fake<ILogger>();
            repository = new AnyAuditedRepository(dbContext, logger);
        }

        [Fact]
        public void Create_AnyAuditedEntity_EntityCreated()
        {
            // Arrange
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

            // Act
            repository.Create(entityToCreate, 1);
            dbContext.SaveChanges();

            // Assert
            var actual = dbContext.AnyAuditedEntities.First();
            actual.Should().BeEquivalentTo(expected, opts => opts.Excluding(x => x.Id).Excluding(x => x.CreatedAt));
            actual.CreatedAt.Should().BeCloseTo(expectedTime);
        }

        [Fact]
        public void Update_AnyAuditedEntity_EntityUpdated()
        {
            // Arrange
            var createdAt = DateTime.Parse($"{DateTime.Today.AddDays(-1):yyyy-MM-dd} 12:00:00");
            dbContext.Add(new AnyAuditedEntity
                {
                    Id = 1,
                    AnyString = "AnyString",
                    CreatedBy = 1,
                    CreatedAt = createdAt
                }
            );
            dbContext.SaveChanges();
            var expectedTime = DateTime.UtcNow;
            var expected = new AnyAuditedEntity
            {
                Id = 1,
                AnyString = "AnyNewString",
                CreatedBy = 1,
                CreatedAt = createdAt,
                UpdatedBy = 1
            };

            // Act
            repository.Update(1, expected, 1);
            dbContext.SaveChanges();

            // Assert
            var actual = dbContext.AnyAuditedEntities.First();
            actual.Should().BeEquivalentTo(expected, opts => opts.Excluding(x => x.UpdatedAt));
            actual.UpdatedAt.Should().BeCloseTo(expectedTime);
        }

        [Fact]
        public async Task UpdateAsync_AnyAuditedEntity_EntityUpdated()
        {
            // Arrange
            var createdAt = DateTime.Parse($"{DateTime.Today.AddDays(-1):yyyy-MM-dd} 12:00:00");
            dbContext.Add(new AnyAuditedEntity
                {
                    Id = 1,
                    AnyString = "AnyString",
                    CreatedBy = 1,
                    CreatedAt = createdAt
                }
            );
            dbContext.SaveChanges();
            var expectedTime = DateTime.UtcNow;
            var expected = new AnyAuditedEntity
            {
                Id = 1,
                AnyString = "AnyNewString",
                CreatedBy = 1,
                CreatedAt = createdAt,
                UpdatedBy = 1
            };

            // Act
            await repository.UpdateAsync(1, expected, 1);
            dbContext.SaveChanges();

            // Assert
            var actual = dbContext.AnyAuditedEntities.First();
            actual.Should().BeEquivalentTo(expected, opts => opts.Excluding(x => x.UpdatedAt));
            actual.UpdatedAt.Should().BeCloseTo(expectedTime);
        }


        [Fact]
        public void InheritedCreate_Any_ThrowsNotSupportedException()
        {
            repository.Invoking(r => r.Create(new AnyAuditedEntity())).Should().Throw<NotSupportedException>();
        }

        [Fact]
        public void InheritedUpdate_Any_ThrowsNotSupportedException()
        {
            repository.Invoking(r => r.Update(1, new AnyAuditedEntity())).Should().Throw<NotSupportedException>();
        }

        [Fact]
        public async Task InheritedUpdateAsync_Any_ThrowsNotSupportedException()
        {
            await repository.Awaiting(r => r.UpdateAsync(1, new AnyAuditedEntity())).Should().ThrowAsync<NotSupportedException>();
        }
    }
}
