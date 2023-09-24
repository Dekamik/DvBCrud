using DvBCrud.API;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.Tests.Mocks.API.WeatherForecasts;

[ApiController]
[Route("api/v1/[controller]Async")]
public class WeatherForecastsAsyncController : AsyncCrudController<int, WeatherForecastModel, IWeatherForecastRepository, WeatherForecastFilter>
{
    public WeatherForecastsAsyncController(IWeatherForecastRepository repository) : base(repository)
    {
    }
}
