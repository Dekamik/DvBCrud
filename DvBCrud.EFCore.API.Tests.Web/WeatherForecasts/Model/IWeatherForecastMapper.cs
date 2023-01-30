using DvBCrud.Common.Services.Mapping;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;

public interface IWeatherForecastMapper : IMapper<WeatherForecast, WeatherForecastModel>
{
}