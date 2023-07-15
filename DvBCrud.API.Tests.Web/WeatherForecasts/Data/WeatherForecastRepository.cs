using DvBCrud.API.Tests.Web.WeatherForecasts.Model;
using DvBCrud.EFCore;

namespace DvBCrud.API.Tests.Web.WeatherForecasts.Data;

public class WeatherForecastRepository : Repository<WeatherForecast, int, WeatherDbContext, WeatherForecastMapper, WeatherForecastModel, WeatherForecastFilter>, IWeatherForecastRepository
{
    public WeatherForecastRepository(WeatherDbContext context, WeatherForecastMapper mapper) : base(context, mapper)
    {
    }
}