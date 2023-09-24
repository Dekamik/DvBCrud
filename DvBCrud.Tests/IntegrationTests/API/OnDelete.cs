using DvBCrud.Tests.Mocks.API;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DvBCrud.Tests.IntegrationTests.API;

public class OnDelete : IntegrationTestBase
{
    public OnDelete(WebApplicationFactory<Startup> factory) : base(factory)
    {
    }
    
    [Theory]
    [InlineData("api/v1/WeatherForecasts/1")]
    [InlineData("api/v1/WeatherForecastsAsync/1")]
    public async Task WithExistingId(string endpoint)
    {
        
    }
}