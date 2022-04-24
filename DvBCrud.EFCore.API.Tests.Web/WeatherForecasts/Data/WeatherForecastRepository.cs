using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Repositories;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data
{
    [ExcludeFromCodeCoverage]
    public class WeatherForecastRepository : Repository<WeatherForecast, int, WeatherDbContext>, IWeatherForecastRepository
    {
        public WeatherForecastRepository(WeatherDbContext context) : base(context)
        {
        }
    }
}
