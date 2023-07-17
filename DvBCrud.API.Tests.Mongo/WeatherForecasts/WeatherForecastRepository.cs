using DvBCrud.Mongo;
using MongoDB.Driver;

namespace DvBCrud.API.Tests.Mongo.WeatherForecasts;

public class WeatherForecastRepository : Repository<WeatherForecast, WeatherForecastMapper, WeatherForecastModel, WeatherForecastFilter>
{
    public WeatherForecastRepository(IMongoCollection<WeatherForecast> collection, WeatherForecastMapper mapper) : base(collection, mapper)
    {
    }
}