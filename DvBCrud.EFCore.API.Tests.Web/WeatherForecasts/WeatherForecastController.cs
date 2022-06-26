using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Service;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts
{
    [ExcludeFromCodeCoverage]
    public class WeatherForecastController : CrudController<int, WeatherForecastModel, IWeatherForecastService>
    {
        public WeatherForecastController(IWeatherForecastService service) : base(service)
        {
        }
    }
}
