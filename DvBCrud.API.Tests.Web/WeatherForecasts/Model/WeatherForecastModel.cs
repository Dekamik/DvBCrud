using System;
using System.Diagnostics.CodeAnalysis;

namespace DvBCrud.API.Tests.Web.WeatherForecasts.Model;

public class WeatherForecastModel
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF
    {
        get { return 32 + (int)(TemperatureC / 0.5556); }
    }

    public string Summary { get; set; } = "";
}