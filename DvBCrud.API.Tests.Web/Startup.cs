using System;
using System.Text.Json.Serialization;
using DvBCrud.API.Swagger;
using DvBCrud.API.Tests.Web.WeatherForecasts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DvBCrud.API.Tests.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<WeatherDbContext>(options =>
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            options.UseSqlite(connection);
        }, ServiceLifetime.Singleton);

        services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
        services.AddScoped<WeatherForecastMapper>();

        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddCrudSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        var context = app.ApplicationServices.GetRequiredService<WeatherDbContext>();
        context.Database.EnsureCreated();
        var forecasts = new[]
        {
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-10T12:00:00+02:00"),
                TemperatureC = 25,
                Summary = "Sunny"
            },
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-10T18:00:00+02:00"),
                TemperatureC = 23,
                Summary = "Sunny"
            },
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-11T00:00:00+02:00"),
                TemperatureC = 17,
                Summary = "Clear"
            },
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-11T06:00:00+02:00"),
                TemperatureC = 16,
                Summary = "Sunny"
            },
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-11T12:00:00+02:00"),
                TemperatureC = 23,
                Summary = "Sunny"
            },
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-11T18:00:00+02:00"),
                TemperatureC = 23,
                Summary = "Cloudy"
            },
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-12T00:00:00+02:00"),
                TemperatureC = 18,
                Summary = "Cloudy"
            },
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-12T06:00:00+02:00"),
                TemperatureC = 17,
                Summary = "Cloudy"
            },
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-12T12:00:00+02:00"),
                TemperatureC = 23,
                Summary = "Overcast"
            },
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-12T18:00:00+02:00"),
                TemperatureC = 20,
                Summary = "Rain"
            },
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-13T00:00:00+02:00"),
                TemperatureC = 16,
                Summary = "Overcast"
            },
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-13T06:00:00+02:00"),
                TemperatureC = 17,
                Summary = "Cloudy"
            },
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-13T12:00:00+02:00"),
                TemperatureC = 22,
                Summary = "Sunny"
            },
            new WeatherForecast
            {
                Date = DateTime.Parse("2023-07-13T18:00:00+02:00"),
                TemperatureC = 21,
                Summary = "Clear"
            }
        };
        context.WeatherForecasts.AddRange(forecasts);
        context.SaveChanges();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}