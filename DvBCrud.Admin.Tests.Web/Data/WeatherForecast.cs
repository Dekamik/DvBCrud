using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DvBCrud.Shared.Entities;

namespace DvBCrud.Admin.Tests.Web.Data;

public class WeatherForecast : IEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    public DateTimeOffset Date { get; set; }

    public int TemperatureC { get; set; }

    public string? Summary { get; set; }
}