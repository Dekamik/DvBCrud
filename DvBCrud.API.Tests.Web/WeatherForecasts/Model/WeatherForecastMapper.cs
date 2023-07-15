using System;
using System.Collections.Generic;
using System.Linq;
using DvBCrud.API.Tests.Web.WeatherForecasts.Data;
using DvBCrud.Shared;

namespace DvBCrud.API.Tests.Web.WeatherForecasts.Model;

public class WeatherForecastMapper : BaseMapper<WeatherForecast, WeatherForecastModel, WeatherForecastFilter>
{
    public override WeatherForecastModel ToModel(WeatherForecast other)
    {
        return new WeatherForecastModel
        {
            Id = other.Id,
            Date = other.Date,
            Summary = other.Summary,
            TemperatureC = other.TemperatureC
        };
    }

    public override WeatherForecast ToEntity(WeatherForecastModel other)
    {
        return new WeatherForecast
        {
            Date = other.Date,
            Summary = other.Summary,
            TemperatureC = other.TemperatureC
        };
    }

    public override void UpdateEntity(WeatherForecast target, WeatherForecast other)
    {
        target.Date = other.Date;
        target.TemperatureC = other.TemperatureC;
        target.Summary = other.Summary;
    }

    public override IEnumerable<WeatherForecast> FilterAndSort(IEnumerable<WeatherForecast> entities, WeatherForecastFilter filter)
    {
        if (filter.Date.HasValue)
            entities = entities.Where(e => e.Date.Date == filter.Date.Value.Date);

        if (!string.IsNullOrWhiteSpace(filter.Summary))
            entities = entities.Where(e => e.Summary.Contains(filter.Summary));

        switch (filter.Order)
        {
            case WeatherForecastFilter.OrderBy.Date:
                entities = entities.OrderBy(e => e.Date);
                break;
            case WeatherForecastFilter.OrderBy.Temperature:
                entities = entities.OrderBy(e => e.TemperatureC);
                break;
            case WeatherForecastFilter.OrderBy.Summary:
                entities = entities.OrderBy(e => e.Summary);
                break;
            case null:
                break;
            default:
                throw new ArgumentOutOfRangeException($"{filter}.{filter.Order}");
        }

        if (filter.Descending.HasValue && filter.Descending.Value)
            entities = entities.Reverse();

        return entities.Skip(filter.Skip)
            .Take(filter.Take);
    }
}