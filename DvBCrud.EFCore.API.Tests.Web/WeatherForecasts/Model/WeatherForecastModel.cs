using System;
using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Services.Models;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;

[ExcludeFromCodeCoverage]
public class WeatherForecastModel : BaseModel
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary { get; set; }
}