using DvBCrud.API.Tests.Web.WeatherForecasts.Data;

namespace DvBCrud.API.Tests.Web.WeatherForecasts.Model;

public class WeatherForecastMapper : IWeatherForecastMapper
{
    public WeatherForecastModel ToModel(WeatherForecast other)
    {
        return new WeatherForecastModel
        {
            Id = other.Id,
            Date = other.Date,
            Summary = other.Summary,
            TemperatureC = other.TemperatureC
        };
    }

    public WeatherForecast ToEntity(WeatherForecastModel other)
    {
        return new WeatherForecast
        {
            Date = other.Date,
            Summary = other.Summary,
            TemperatureC = other.TemperatureC
        };
    }

    public void UpdateEntity(WeatherForecast target, WeatherForecast other)
    {
        target.Date = other.Date;
        target.TemperatureC = other.TemperatureC;
        target.Summary = other.Summary;
    }
}