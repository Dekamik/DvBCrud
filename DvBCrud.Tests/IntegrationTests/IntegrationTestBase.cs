using DvBCrud.Tests.Mocks.API;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DvBCrud.Tests.IntegrationTests;

public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
{
    private readonly SqliteConnection _connection;
    protected readonly WeatherDbContext DbContext;
    protected readonly WebApplicationFactory<Startup> Factory;
    protected readonly MockDateTimeOffsetFactory MockDateTimeOffsetFactory;

    protected IntegrationTestBase(WebApplicationFactory<Startup> factory)
    {
        Factory = factory;
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        
        var options = new DbContextOptionsBuilder<WeatherDbContext>()
            .UseSqlite(_connection)
            .Options;
        MockDateTimeOffsetFactory = new MockDateTimeOffsetFactory();
        DbContext = new WeatherDbContext(options/*, MockDateTimeOffsetFactory*/);
        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _connection.Dispose();
        DbContext.Dispose();
    }
}