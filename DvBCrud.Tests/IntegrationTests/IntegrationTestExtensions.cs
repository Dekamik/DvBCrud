using DvBCrud.Tests.Mocks.API;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DvBCrud.Tests.IntegrationTests;

public static class IntegrationTestExtensions
{
    public static TReturn ExtractJsonValue<TObject, TReturn>(this string json, Func<TObject, TReturn> selector) => 
        selector.Invoke(JsonConvert.DeserializeObject<TObject>(json)!);

    public static string FormatJson(this string json) =>
        JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json), Formatting.Indented, 
            new JsonSerializerSettings 
            { 
                DateTimeZoneHandling = DateTimeZoneHandling.Unspecified 
            });

    public static WebApplicationFactory<Startup> InjectDbContext(this WebApplicationFactory<Startup> factory, WeatherDbContext dbContext) =>
        factory.WithWebHostBuilder(builder => builder.ConfigureServices(
            services =>
            {
                var descriptor = services.First(e => e.ServiceType == typeof(DbContextOptions<WeatherDbContext>));
                services.Remove(descriptor);
                services.AddSingleton(dbContext);
            }));
}