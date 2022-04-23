using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Tests.Web.Weather
{
    [ExcludeFromCodeCoverage]
    public class WeatherForecastRepository : Repository<WeatherForecast, int, WebDbContext>, IWeatherForecastRepository
    {
        public WeatherForecastRepository(WebDbContext context) : base(context)
        {
        }
    }
}
