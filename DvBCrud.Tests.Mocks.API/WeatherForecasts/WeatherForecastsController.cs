using DvBCrud.API;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.Tests.Mocks.API.WeatherForecasts;

[ApiController]
[Route("api/v1/[controller]")]
public class WeatherForecastsController : CrudController<int, WeatherForecastModel, IWeatherForecastRepository, WeatherForecastFilter>
{
    public WeatherForecastsController(IWeatherForecastRepository repository) : base(repository)
    {
    }
}
