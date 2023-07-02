using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;
using DvBCrud.EFCore.Repositories;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data
{
    [ExcludeFromCodeCoverage]
    public class WeatherForecastRepository : Repository<WeatherForecast, int, WeatherDbContext, WeatherForecastMapper, WeatherForecastModel>, IWeatherForecastRepository
    {
        public WeatherForecastRepository(WeatherDbContext context, WeatherForecastMapper mapper) : base(context, mapper)
        {
        }
    }
}
