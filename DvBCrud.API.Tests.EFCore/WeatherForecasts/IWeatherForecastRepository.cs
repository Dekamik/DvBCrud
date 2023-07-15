using DvBCrud.Shared;

namespace DvBCrud.API.Tests.EFCore.WeatherForecasts;

public interface IWeatherForecastRepository : IRepository<int, WeatherForecastModel, WeatherForecastFilter>
{
}