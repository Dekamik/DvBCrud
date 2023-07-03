using System.Diagnostics.CodeAnalysis;
using DvBCrud.API.Tests.Web.WeatherForecasts.Data;
using DvBCrud.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.API.Tests.Web
{
    [ExcludeFromCodeCoverage]
    public class WeatherDbContext : CrudDbContext
    {
        public WeatherDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
