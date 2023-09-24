using DvBCrud.Tests.UnitTests.EFCore.Mocks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.Tests.UnitTests;

public abstract class SqliteTestBase : IDisposable
{
    private readonly SqliteConnection _connection;
    protected readonly AnyDbContext DbContext;

    protected SqliteTestBase()
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