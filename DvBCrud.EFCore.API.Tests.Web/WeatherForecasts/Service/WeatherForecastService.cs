using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;
using DvBCrud.EFCore.Services;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Service;

[ExcludeFromCodeCoverage]
public class WeatherForecastService : Service<WeatherForecast, int, IWeatherForecastRepository, WeatherForecastModel, IWeatherForecastConverter>, IWeatherForecastService
{
    public WeatherForecastService(IWeatherForecastRepository repository, IWeatherForecastConverter converter) : base(repository, converter)
    {
    }
}