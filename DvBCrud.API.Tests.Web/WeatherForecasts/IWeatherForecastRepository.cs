using DvBCrud.Shared;

namespace DvBCrud.API.Tests.Web.WeatherForecasts;

public interface IWeatherForecastRepository : IRepository<int, WeatherForecastModel, WeatherForecastFilter>
{
}