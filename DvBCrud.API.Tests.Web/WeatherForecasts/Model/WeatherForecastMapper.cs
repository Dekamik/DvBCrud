using System.Diagnostics.CodeAnalysis;
using DvBCrud.API.Tests.Web.WeatherForecasts.Data;

namespace DvBCrud.API.Tests.Web.WeatherForecasts.Model;

[ExcludeFromCodeCoverage]
public class WeatherForecastMapper : IWeatherForecastMapper
{
    public WeatherForecastModel ToModel(WeatherForecast entity) =>
        new()
        {
            Id = entity.Id,
            Date = entity.Date,
            Summary = entity.Summary,
            TemperatureC = entity.TemperatureC
        };

    public WeatherForecast ToEntity(WeatherForecastModel model) =>
        new()
        {
            Date = model.Date,
            Summary = model.Summary,
            TemperatureC = model.TemperatureC
        };

    public void UpdateEntity(WeatherForecast source, WeatherForecast destination)
    {
        destination.Date = source.Date;
        destination.TemperatureC = source.TemperatureC;
        destination.Summary = source.Summary;
    }
}