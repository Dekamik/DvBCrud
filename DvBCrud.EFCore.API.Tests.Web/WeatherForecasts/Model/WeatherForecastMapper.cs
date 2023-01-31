using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;

[ExcludeFromCodeCoverage]
public class WeatherForecastMapper : IWeatherForecastMapper
{
    private readonly IWeatherForecastRepository _repository;
    
    public WeatherForecastMapper(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }
    
    public WeatherForecastModel ToModel(WeatherForecast entity) =>
        new()
        {
            Id = entity.Id,
            Date = entity.Date,
            Summary = entity.Summary,
            TemperatureC = entity.TemperatureC
        };

    public WeatherForecast ToEntity(WeatherForecastModel model)
    {
        var weatherForecast = model.Id != default ? 
            _repository.Get(model.Id) ?? new WeatherForecast() : 
            new WeatherForecast();

        weatherForecast.Date = model.Date;
        weatherForecast.Summary = model.Summary;
        weatherForecast.TemperatureC = model.TemperatureC;
        
        return weatherForecast;
    }
}