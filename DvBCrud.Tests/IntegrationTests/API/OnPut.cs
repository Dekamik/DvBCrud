using DvBCrud.Tests.Mocks.API;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DvBCrud.Tests.IntegrationTests.API;

public class OnPut : IntegrationTestBase
{
    public OnPut(WebApplicationFactory<Startup> factory) : base(factory)
    {
    }
    
    [Theory]
    [InlineData("api/v1/WeatherForecasts")]
    [InlineData("api/v1/WeatherForecastsAsync")]
    public async Task WithExistingIdAndModifiedModel(string endpoint)
    {
        
    }
}