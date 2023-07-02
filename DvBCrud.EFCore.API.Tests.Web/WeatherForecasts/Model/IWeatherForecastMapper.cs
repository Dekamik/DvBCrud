using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data;
using DvBCrud.EFCore.Mapping;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;

public interface IWeatherForecastMapper : IMapper<WeatherForecast, WeatherForecastModel>
{
}