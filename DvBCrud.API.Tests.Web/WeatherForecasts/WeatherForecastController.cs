using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.API.Tests.Web.WeatherForecasts;

[ApiController]
[Route("api/v1/[controller]")]
public class WeatherForecastController : CrudController<int, WeatherForecastModel, IWeatherForecastRepository, WeatherForecastFilter>
{
    public WeatherForecastController(IWeatherForecastRepository repository) : base(repository)
    {
    }
}