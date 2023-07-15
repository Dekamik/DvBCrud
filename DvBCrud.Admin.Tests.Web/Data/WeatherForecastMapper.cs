using DvBCrud.Shared;

namespace DvBCrud.Admin.Tests.Web.Data;

public class WeatherForecastMapper : IMapper<WeatherForecast, WeatherForecastModel, WeatherForecastFilter>
{
    public WeatherForecastModel ToModel(WeatherForecast entity)
    {
        return new WeatherForecastModel
        {
            Id = entity.Id,
            Date = entity.Date,
            TemperatureC = entity.TemperatureC,
            Summary = entity.Summary
        };
    }

    public WeatherForecast ToEntity(WeatherForecastModel entity)
    {
        return new WeatherForecast
        {
            Date = entity.Date,
            TemperatureC = entity.TemperatureC,
            Summary = entity.Summary
        };
    }

    public void UpdateEntity(WeatherForecast destination, WeatherForecast source)
    {
        destination.Date = source.Date;
        destination.TemperatureC = source.TemperatureC;
        destination.Summary = source.Summary;
    }

    public IEnumerable<WeatherForecast> FilterOrderAndPaginate(IEnumerable<WeatherForecast> entities, WeatherForecastFilter filter)
    {
        return entities;
    }
}