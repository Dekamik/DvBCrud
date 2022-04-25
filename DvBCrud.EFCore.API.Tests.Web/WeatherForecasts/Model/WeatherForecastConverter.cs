using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data;
using DvBCrud.EFCore.Services.Models;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;

[ExcludeFromCodeCoverage]
public class WeatherForecastConverter : Converter<WeatherForecast, WeatherForecastModel>, IWeatherForecastConverter
{
    public override WeatherForecastModel ToModel(WeatherForecast entity) =>
        new()
        {
            Id = entity.Id,
            Date = entity.Date,
            Summary = entity.Summary,
            TemperatureC = entity.TemperatureC
        };

    public override WeatherForecast ToEntity(WeatherForecastModel model) =>
        new()
        {
            Id = model.Id,
            Date = model.Date,
            Summary = model.Summary,
            TemperatureC = model.TemperatureC
        };
}