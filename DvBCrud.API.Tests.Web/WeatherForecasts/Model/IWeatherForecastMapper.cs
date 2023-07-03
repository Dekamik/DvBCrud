using DvBCrud.API.Tests.Web.WeatherForecasts.Data;
using DvBCrud.Shared;

namespace DvBCrud.API.Tests.Web.WeatherForecasts.Model;

public interface IWeatherForecastMapper : IMapper<WeatherForecast, WeatherForecastModel>
{
}