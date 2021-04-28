using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Tests.Web.Weather;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.EFCore.API.Tests.Web
{
    [ExcludeFromCodeCoverage]
    public class WebDbContext : DbContext
    {
        public WebDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
