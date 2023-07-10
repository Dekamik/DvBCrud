using System.ComponentModel.DataAnnotations;
using DvBCrud.Shared.Entities;

namespace DvBCrud.Admin.Tests.Web.Data;

public class WeatherForecastModel : IEntity<long>
{
    public long Id { get; set; }
    
    public DateTimeOffset Date { get; set; }

    [Display(Name = "°C")]
    public int TemperatureC { get; set; }
    
    [Display(Name = "°F")]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}