using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.EFCore.Tests.Mocks;

public class AnyDbContextFixture : IDisposable
{
    private readonly SqliteConnection _connection;
    public readonly AnyDbContext DbContext;

    public AnyDbContextFixture()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        
        var options = new DbContextOptionsBuilder<AnyDbContext>()
            .UseSqlite(_connection)
            .Options;
        DbContext = new AnyDbContext(options);
        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _connection.Dispose();
        DbContext.Dispose();
    }
}