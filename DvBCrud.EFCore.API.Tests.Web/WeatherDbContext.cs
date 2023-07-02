using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.EFCore.API.Tests.Web
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
