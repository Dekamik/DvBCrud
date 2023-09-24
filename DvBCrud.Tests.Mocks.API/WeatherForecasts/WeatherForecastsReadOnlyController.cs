using DvBCrud.API;
using DvBCrud.Shared.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.Tests.Mocks.API.WeatherForecasts;

[ApiController]
[Route("api/v1/[controller]RO")]
[AllowedActions(CrudActions.ReadOnly)]
public class WeatherForecastsReadOnlyController : CrudController<int, WeatherForecastModel, IWeatherForecastRepository, WeatherForecastFilter>
{
    public WeatherForecastsReadOnlyController(IWeatherForecastRepository repository) : base(repository)
    {
    }
}
