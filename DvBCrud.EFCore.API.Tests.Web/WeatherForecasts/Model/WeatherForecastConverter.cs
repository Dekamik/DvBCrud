using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data;
using DvBCrud.EFCore.Services.Models;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;

[ExcludeFromCodeCoverage]
public class WeatherForecastConverter : Converter<WeatherForecast, WeatherForecastModel>, IWeatherForecastConverter
{
    private readonly IWeatherForecastRepository _repository;
    
    public WeatherForecastConverter(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }
    
    public override WeatherForecastModel ToModel(WeatherForecast entity) =>
        new()
        {
            Id = entity.Id,
            Date = entity.Date,
            Summary = entity.Summary,
            TemperatureC = entity.TemperatureC
        };

    public override WeatherForecast ToEntity(WeatherForecastModel model)
    {
        var weatherForecast = _repository.Get(model.Id) ?? new WeatherForecast();

        weatherForecast.Date = model.Date;
        weatherForecast.Summary = model.Summary;
        weatherForecast.TemperatureC = model.TemperatureC;
        
        return weatherForecast;
    }
}