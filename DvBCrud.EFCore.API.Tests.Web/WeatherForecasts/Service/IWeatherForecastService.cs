using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;
using DvBCrud.EFCore.Services;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Service;

public interface IWeatherForecastService : IService<int, WeatherForecastModel>
{
}