using DvBCrud.Shared;

namespace DvBCrud.Admin.Tests.Web.Data;

public class WeatherForecastMapper : BaseMapper<WeatherForecast, WeatherForecastModel, WeatherForecastFilter>
{
    public override WeatherForecastModel ToModel(WeatherForecast entity)
    {
        return new WeatherForecastModel
        {
            Id = entity.Id,
            Date = entity.Date,
            TemperatureC = entity.TemperatureC,
            Summary = entity.Summary
        };
    }

    public override WeatherForecast ToEntity(WeatherForecastModel entity)
    {
        return new WeatherForecast
        {
            Date = entity.Date,
            TemperatureC = entity.TemperatureC,
            Summary = entity.Summary
        };
    }

    public override void UpdateEntity(WeatherForecast destination, WeatherForecast source)
    {
        destination.Date = source.Date;
        destination.TemperatureC = source.TemperatureC;
        destination.Summary = source.Summary;
    }
}