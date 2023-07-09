﻿using System.Diagnostics.CodeAnalysis;
using DvBCrud.API.Tests.Web.WeatherForecasts.Model;
using DvBCrud.EFCore;

namespace DvBCrud.API.Tests.Web.WeatherForecasts.Data;

[ExcludeFromCodeCoverage]
public class WeatherForecastRepository : Repository<WeatherForecast, int, WeatherDbContext, IWeatherForecastMapper, WeatherForecastModel>, IWeatherForecastRepository
{
    public WeatherForecastRepository(WeatherDbContext context, IWeatherForecastMapper mapper) : base(context, mapper)
    {
    }
}