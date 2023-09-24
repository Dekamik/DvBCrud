using System;

namespace DvBCrud.Tests.Mocks.API.WeatherForecasts;

public class WeatherForecastFilter
{
    public enum OrderBy
    {
        Date = 1,
        Temperature = 2,
        Summary = 3,
    }

    public OrderBy? Order { get; set; }
    public bool? Descending { get; set; }
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;

    public DateTime? Date { get; set; }
    public string? Summary { get; set; }
}