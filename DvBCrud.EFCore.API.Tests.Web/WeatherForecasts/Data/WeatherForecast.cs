using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Entities;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data
{
    [ExcludeFromCodeCoverage]
    public class WeatherForecast : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; } = "";
    }
}
