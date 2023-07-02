using DvBCrud.EFCore.API.Handlers;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Service;

public interface IWeatherForecastCrudHandler : ICrudHandler<int, WeatherForecastModel>
{
}