using System;
using System.Text.Json.Serialization;
using DvBCrud.API.Swagger;
using DvBCrud.Tests.Mocks.API.WeatherForecasts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DvBCrud.Tests.Mocks.API;

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
        
        services.AddCrudSwaggerGen();

        services.AddControllers().AddJsonOptions(options => 
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
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