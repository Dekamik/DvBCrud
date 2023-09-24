using DvBCrud.EFCore;

namespace DvBCrud.Tests.Mocks.API.WeatherForecasts;

public class WeatherForecastRepository : Repository<WeatherForecast, int, WeatherDbContext, WeatherForecastMapper, WeatherForecastModel, WeatherForecastFilter>, IWeatherForecastRepository
{
    public WeatherForecastRepository(WeatherDbContext context, WeatherForecastMapper mapper) : base(context, mapper)
    {
    }
}