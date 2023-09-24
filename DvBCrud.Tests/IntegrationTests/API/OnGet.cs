using System.Net;
using DvBCrud.Tests.Mocks.API;
using DvBCrud.Tests.Mocks.API.WeatherForecasts;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DvBCrud.Tests.IntegrationTests.API;

public class OnGet : IntegrationTestBase
{
    public OnGet(WebApplicationFactory<Startup> factory) : base(factory)
    {
    }
    
    [Theory]
    [InlineData("api/v1/WeatherForecasts/1")]
    [InlineData("api/v1/WeatherForecastsAsync/1")]
    public async Task WithExistingId(string url)
    {
        DbContext.WeatherForecasts.Add(
            new WeatherForecast 
            {
                Id = 1,
                Date = DateTime.Parse("2023-09-24T12:00:00"),
                TemperatureC = 17,
                Summary = "Clear"
            });
        
        var client = Factory.InjectDbContext(DbContext).CreateClient();
        var response = await client.GetAsync(url);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var actual = await response.Content.ReadAsStringAsync();
        var expected = await File.ReadAllTextAsync("IntegrationTests/API/Contracts/onget_withexistingid_response.json");
        actual.FormatJson().Should().Be(expected);
    }

    [Theory]
    [InlineData("api/v1/WeatherForecasts")]
    [InlineData("api/v1/WeatherForecastsAsync")]
    public async Task WithoutId(string url)
    {
        DbContext.WeatherForecasts.AddRange(
            new WeatherForecast 
            {
                Id = 1,
                Date = DateTime.Parse("2023-09-24T12:00:00"),
                TemperatureC = 17,
                Summary = "Clear"
            },
            new WeatherForecast
            {
                Id = 2,
                Date = DateTime.Parse("2023-09-24T18:00:00"),
                TemperatureC = 16,
                Summary = "Clear"
            }
        );
        
        var client = Factory.InjectDbContext(DbContext).CreateClient();
        var response = await client.GetAsync(url);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var actual = await response.Content.ReadAsStringAsync();
        var expected = await File.ReadAllTextAsync("IntegrationTests/API/Contracts/onget_withoutid_response.json");
        actual.FormatJson().Should().Be(expected);
    }
}
