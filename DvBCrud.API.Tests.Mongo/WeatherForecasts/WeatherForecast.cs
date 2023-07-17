using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DvBCrud.Shared.Entities;
using MongoDB.Bson;

namespace DvBCrud.API.Tests.Mongo.WeatherForecasts;

public record WeatherForecast : IEntity<ObjectId>
{
    public ObjectId Id { get; set; }
        
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public string Summary { get; set; } = "";
}