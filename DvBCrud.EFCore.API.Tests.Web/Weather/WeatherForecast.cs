using DvBCrud.EFCore.Entities;
using System;

namespace DvBCrud.EFCore.API.Tests.Web.Weather
{
    public class WeatherForecast : BaseEntity<int>
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

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
