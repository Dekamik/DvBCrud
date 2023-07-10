using System.Diagnostics.CodeAnalysis;
using DvBCrud.API.Tests.Web.WeatherForecasts.Data;
using DvBCrud.API.Tests.Web.WeatherForecasts.Model;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.API.Tests.Web.WeatherForecasts;

[ApiController]
[Route("api/v1/[controller]")]
public class WeatherForecastController : CrudController<int, WeatherForecastModel, IWeatherForecastRepository>
{
    public WeatherForecastController(IWeatherForecastRepository crudHandler) : base(crudHandler)
    {
    }
}