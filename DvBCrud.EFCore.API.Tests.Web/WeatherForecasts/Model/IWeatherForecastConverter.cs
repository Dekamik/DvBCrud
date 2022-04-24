using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data;
using DvBCrud.EFCore.Services.Models;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;

public interface IWeatherForecastConverter : IConverter<WeatherForecast, WeatherForecastModel>
{
}