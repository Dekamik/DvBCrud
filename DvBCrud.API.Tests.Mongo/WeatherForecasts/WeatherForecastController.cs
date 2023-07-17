using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DvBCrud.API.Tests.Mongo.WeatherForecasts;

[ApiController]
[Route("api/v1/[controller]")]
public class WeatherForecastController : CrudController<ObjectId, WeatherForecastModel, WeatherForecastRepository, WeatherForecastFilter>
{
    public WeatherForecastController(WeatherForecastRepository repository) : base(repository)
    {
    }
}