using System;
using DvBCrud.EFCore.Tests.Mocks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DvBCrud.EFCore.Tests;

public class CrudDbContextTests
{
    private readonly AnyDbContextFixture _dbContextFixture;

    public CrudDbContextTests(AnyDbContextFixture dbContextFixture)
    {
        _dbContextFixture = dbContextFixture;
    }
    
    [Fact]
    public void ModifyTimestamps_CreatedEntityHasCreatedAt_SetsCreatedAtToUtcNow()
    {
        var entity = new AnyEntity
        {
            AnyString = "AnyString"
        };
        
        _dbContextFixture.DbContext.Add(entity);
        _dbContextFixture.DbContext.SaveChanges();
        var now = DateTimeOffset.UtcNow;

        entity.CreatedAt.Should().BeCloseTo(now, TimeSpan.FromMilliseconds(100));
    }

    [Fact]
    public void ModifyTimestamps_CreatedEntityHasModifiedAt_SetsModifiedAtToUtcNow()
    {
        var entity = new AnyEntity
        {
            AnyString = "AnyString"
        };
        
        _dbContextFixture.DbContext.Add(entity);
        _dbContextFixture.DbContext.SaveChanges();
        var now = DateTimeOffset.UtcNow;

        entity.ModifiedAt.Should().BeCloseTo(now, TimeSpan.FromMilliseconds(100));
    }

    [Fact]
    public void ModifyTimestamps_CreatedEntityHasBothCreatedAtAndModifiedAt_BothDatesMatchExactly()
    {
        var entity = new AnyEntity
        {
            AnyString = "AnyString"
        };
        
        _dbContextFixture.DbContext.Add(entity);
        _dbContextFixture.DbContext.SaveChanges();

        entity.CreatedAt.Should().Be(entity.ModifiedAt);
    }

    [Fact]
    public void ModifyTimestamps_UpdatedEntityHasModifiedAt_SetsModifiedAtToUtcNow()
    {
        var entity = new AnyEntity
        {
            AnyString = "AnyString"
        };
        _dbContextFixture.DbContext.Add(entity);
        _dbContextFixture.DbContext.SaveChanges();
        entity.ModifiedAt.Should().Be(entity.CreatedAt);

        entity.AnyString = "AnyOtherString";
        _dbContextFixture.DbContext.SaveChanges();
        entity.ModifiedAt.Should().NotBe(entity.CreatedAt);
    }
}