using DvBCrud.EFCore.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Tests.Web.Weather
{
    public class WeatherForecastRepository : Repository<WeatherForecast, int, WebDbContext>, IWeatherForecastRepository
    {
        public WeatherForecastRepository(WebDbContext context, ILogger<WeatherForecastRepository> logger) : base(context, logger)
        {
        }
    }
}
