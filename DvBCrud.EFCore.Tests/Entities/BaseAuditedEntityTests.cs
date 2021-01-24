using DvBCrud.EFCore.Mocks.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace DvBCrud.EFCore.Tests.Entities
{
    public class BaseAuditedEntityTests
    {
        [Fact]
        public void Copy_AnyValidEntity_DefinedValuesAndUpdatedValuesCopied()
        {
            var originalCreatedAt = DateTime.Parse($"{DateTime.Today.AddDays(-1):yyyy-MM-dd} 12:00:00");
            var newCreatedAt = DateTime.Parse($"{DateTime.Today.AddDays(-1):yyyy-MM-dd} 14:00:00");
            var originalUpdatedAt = DateTime.Parse($"{DateTime.Today.AddDays(-1):yyyy-MM-dd} 18:00:00");
            var newUpdatedAt = DateTime.Parse($"{DateTime.Today.AddDays(-1):yyyy-MM-dd} 20:00:00");
            var original = new AnyAuditedEntity
            {
                Id = 1,
                AnyString = "AnyString",
                CreatedAt = originalCreatedAt,
                CreatedBy = 1,
                UpdatedAt = originalUpdatedAt,
                UpdatedBy = 2
            };
            var newEntity = new AnyAuditedEntity
            {
                Id = 2,
                AnyString = "AnyString",
                CreatedAt = newCreatedAt,
                CreatedBy = 2,
                UpdatedAt = newUpdatedAt,
                UpdatedBy = 3
            };

            newEntity.Copy(original);

            newEntity.Id.Should().NotBe(original.Id);
            newEntity.AnyString.Should().Be(original.AnyString);
            newEntity.CreatedAt.Should().Be(newCreatedAt);
            newEntity.CreatedBy.Should().Be(2);
            newEntity.UpdatedAt.Should().Be(originalUpdatedAt);
            newEntity.UpdatedBy.Should().Be(2);
        }

        [Fact]
        public void Copy_EntityNotDerivedFromBaseAuditedEntity_ThrowsArgumentException()
        {
            var original = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            var newEntity = new AnyAuditedEntity
            {
                Id = 2,
                AnyString = "AnyString",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 2,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = 3
            };

            newEntity.Invoking(e => e.Copy(original)).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Copy_NullArgument_ThrowsArgumentNullException()
        {
            var newEntity = new AnyAuditedEntity
            {
                Id = 2,
                AnyString = "AnyString",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 2,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = 3
            };

            newEntity.Invoking(e => e.Copy(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
