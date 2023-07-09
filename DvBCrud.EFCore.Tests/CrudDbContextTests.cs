using System;
using DvBCrud.EFCore.Tests.Mocks;
using FluentAssertions;
using Xunit;

namespace DvBCrud.EFCore.Tests;

public class CrudDbContextTests : SqliteTestBase
{
    [Fact]
    public void ModifyTimestamps_CreatedEntityHasCreatedAt_SetsCreatedAtToUtcNow()
    {
        var entity = new AnyEntity
        {
            AnyString = "AnyString"
        };
        
        DbContext.Add(entity);
        DbContext.SaveChanges();
        var now = DateTimeOffset.UtcNow;

        entity.CreatedAt.Should().BeCloseTo(now, TimeSpan.FromMilliseconds(1000));
    }

    [Fact]
    public void ModifyTimestamps_CreatedEntityHasModifiedAt_SetsModifiedAtToUtcNow()
    {
        var entity = new AnyEntity
        {
            AnyString = "AnyString"
        };
        
        DbContext.Add(entity);
        DbContext.SaveChanges();
        var now = DateTimeOffset.UtcNow;

        entity.ModifiedAt.Should().BeCloseTo(now, TimeSpan.FromMilliseconds(1000));
    }

    [Fact]
    public void ModifyTimestamps_CreatedEntityHasBothCreatedAtAndModifiedAt_BothDatesMatchExactly()
    {
        var entity = new AnyEntity
        {
            AnyString = "AnyString"
        };
        
        DbContext.Add(entity);
        DbContext.SaveChanges();

        entity.CreatedAt.Should().Be(entity.ModifiedAt);
    }

    [Fact]
    public void ModifyTimestamps_UpdatedEntityHasModifiedAt_SetsModifiedAtToUtcNow()
    {
        var entity = new AnyEntity
        {
            AnyString = "AnyString"
        };
        DbContext.Add(entity);
        DbContext.SaveChanges();
        entity.ModifiedAt.Should().Be(entity.CreatedAt);

        entity.AnyString = "AnyOtherString";
        DbContext.SaveChanges();
        entity.ModifiedAt.Should().NotBe(entity.CreatedAt);
    }
}