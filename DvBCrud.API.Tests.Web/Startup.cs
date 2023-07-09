using System.Diagnostics.CodeAnalysis;
using DvBCrud.API.Swagger;
using DvBCrud.API.Tests.Web.WeatherForecasts.Data;
using DvBCrud.API.Tests.Web.WeatherForecasts.Model;
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
        services.AddScoped<IWeatherForecastMapper, WeatherForecastMapper>();

        services.AddControllers();

        services.AddCrudSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.ApplicationServices.GetRequiredService<WeatherDbContext>()
            .Database
            .EnsureCreated();

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