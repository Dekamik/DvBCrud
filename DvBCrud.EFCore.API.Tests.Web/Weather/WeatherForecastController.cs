using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Tests.Web.Weather
{
    [ExcludeFromCodeCoverage]
    public class WeatherForecastController : CrudController<WeatherForecast, int, IWeatherForecastRepository>
    {
        public WeatherForecastController(IWeatherForecastRepository repository) : base(repository)
        {

        }
    }
}
