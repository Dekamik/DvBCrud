using System;
using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Entities;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data
{
    [ExcludeFromCodeCoverage]
    public class WeatherForecast : BaseEntity<int>
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        protected override void CopyImpl(BaseEntity<int> other)
        {
            WeatherForecast o = other as WeatherForecast;
            Date = o.Date;
            TemperatureC = o.TemperatureC;
            Summary = o.Summary;
        }
    }
}
