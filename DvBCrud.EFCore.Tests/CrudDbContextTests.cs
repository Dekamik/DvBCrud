using System;
using DvBCrud.EFCore.Tests.Mocks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DvBCrud.EFCore.Tests;

public class CrudDbContextTests
{
    private readonly AnyDbContext _dbContext;

    public CrudDbContextTests()
    {
        var options = new DbContextOptionsBuilder<AnyDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _dbContext = new AnyDbContext(options);
    }
    
    [Fact]
    public void ModifyTimestamps_CreatedEntityHasCreatedAt_SetsCreatedAtToUtcNow()
    {
        var entity = new AnyEntity
        {
            AnyString = "AnyString"
        };
        
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
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
        
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
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
        
        _dbContext.Add(entity);
        _dbContext.SaveChanges();

        entity.CreatedAt.Should().Be(entity.ModifiedAt);
    }

    [Fact]
    public void ModifyTimestamps_UpdatedEntityHasModifiedAt_SetsModifiedAtToUtcNow()
    {
        var entity = new AnyEntity
        {
            AnyString = "AnyString"
        };
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        entity.ModifiedAt.Should().Be(entity.CreatedAt);

        entity.AnyString = "AnyOtherString";
        _dbContext.SaveChanges();
        entity.ModifiedAt.Should().NotBe(entity.CreatedAt);
    }
}