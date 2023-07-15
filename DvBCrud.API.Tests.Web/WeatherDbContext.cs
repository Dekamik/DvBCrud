using DvBCrud.API.Tests.Web.WeatherForecasts;
using DvBCrud.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.API.Tests.Web;

public class WeatherDbContext : CrudDbContext
{
    public WeatherDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;
}