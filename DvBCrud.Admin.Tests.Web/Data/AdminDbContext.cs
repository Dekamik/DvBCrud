using DvBCrud.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DvBCrud.Admin.Tests.Web.Data;

public class AdminDbContext : CrudDbContext
{
    public AdminDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;
}