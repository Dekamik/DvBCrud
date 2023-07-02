using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts
{
    [ExcludeFromCodeCoverage]
    public class WeatherForecastController : CrudController<int, WeatherForecastModel, IWeatherForecastRepository>
    {
        public WeatherForecastController(IWeatherForecastRepository crudHandler) : base(crudHandler)
        {
        }
    }
}
