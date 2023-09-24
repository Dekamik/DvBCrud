using DvBCrud.EFCore;
using DvBCrud.Tests.Mocks.API.WeatherForecasts;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.Tests.Mocks.API;

public class WeatherDbContext : CrudDbContext
{
    public WeatherDbContext(DbContextOptions options) : base(options)
    {

    }
    
    public WeatherDbContext(DbContextOptions options, IDateTimeOffsetFactory dateTimeOffsetFactory) 
        : base(options, dateTimeOffsetFactory)
    {

    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;
}