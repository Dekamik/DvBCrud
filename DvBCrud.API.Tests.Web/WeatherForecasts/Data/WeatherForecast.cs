using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DvBCrud.Shared.Entities;

namespace DvBCrud.API.Tests.Web.WeatherForecasts.Data;

public record WeatherForecast : IEntity<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
        
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public string Summary { get; set; } = "";
}