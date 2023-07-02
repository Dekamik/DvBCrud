using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Handlers;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Service;

[ExcludeFromCodeCoverage]
public class WeatherForecastCrudHandler : CrudHandler<int, IWeatherForecastRepository, WeatherForecastModel>, IWeatherForecastCrudHandler
{
    public WeatherForecastCrudHandler(IWeatherForecastRepository repository) : base(repository)
    {
    }
}