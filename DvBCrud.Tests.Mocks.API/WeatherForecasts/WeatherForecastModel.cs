using System;

namespace DvBCrud.Tests.Mocks.API.WeatherForecasts;

public class WeatherForecastModel
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary { get; set; } = "";
}