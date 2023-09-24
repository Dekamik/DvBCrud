using DvBCrud.Shared;

namespace DvBCrud.Tests.Mocks.API.WeatherForecasts;

public interface IWeatherForecastRepository : IRepository<int, WeatherForecastModel, WeatherForecastFilter>
{
}