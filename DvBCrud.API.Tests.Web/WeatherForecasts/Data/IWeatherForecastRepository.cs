using DvBCrud.API.Tests.Web.WeatherForecasts.Model;
using DvBCrud.Shared;

namespace DvBCrud.API.Tests.Web.WeatherForecasts.Data;

public interface IWeatherForecastRepository : IRepository<int, WeatherForecastModel, WeatherForecastFilter>
{
}