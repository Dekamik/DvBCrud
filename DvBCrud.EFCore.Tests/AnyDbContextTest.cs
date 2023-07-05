using System;
using DvBCrud.EFCore.Tests.Mocks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.EFCore.Tests;

public abstract class AnyDbContextTest : IDisposable
{
    private readonly SqliteConnection _connection;
    protected readonly AnyDbContext DbContext;

    protected AnyDbContextTest()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        
        var options = new DbContextOptionsBuilder<AnyDbContext>()
            .UseSqlite(_connection)
            .Options;
        DbContext = new AnyDbContext(options);
        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _connection.Dispose();
        DbContext.Dispose();
    }
}