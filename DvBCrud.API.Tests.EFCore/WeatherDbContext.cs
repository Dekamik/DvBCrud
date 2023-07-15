using DvBCrud.API.Tests.EFCore.WeatherForecasts;
using DvBCrud.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.API.Tests.EFCore;

public class WeatherDbContext : CrudDbContext
{
    public WeatherDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;
}