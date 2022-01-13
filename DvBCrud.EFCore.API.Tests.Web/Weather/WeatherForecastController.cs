﻿using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.XMLJSON;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Tests.Web.Weather
{
    [ExcludeFromCodeCoverage]
    public class WeatherForecastController : CRUDController<WeatherForecast, int, IWeatherForecastRepository, WebDbContext>
    {
        public WeatherForecastController(IWeatherForecastRepository repository, ILogger<WeatherForecastController> logger) : base(repository, logger)
        {

        }
    }
}
