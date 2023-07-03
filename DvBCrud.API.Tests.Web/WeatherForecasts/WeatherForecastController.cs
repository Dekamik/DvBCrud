using System.Diagnostics.CodeAnalysis;
using DvBCrud.API.Tests.Web.WeatherForecasts.Data;
using DvBCrud.API.Tests.Web.WeatherForecasts.Model;

namespace DvBCrud.API.Tests.Web.WeatherForecasts;

[ExcludeFromCodeCoverage]
public class WeatherForecastController : CrudController<int, WeatherForecastModel, IWeatherForecastRepository>
{
    public WeatherForecastController(IWeatherForecastRepository crudHandler) : base(crudHandler)
    {
    }
}