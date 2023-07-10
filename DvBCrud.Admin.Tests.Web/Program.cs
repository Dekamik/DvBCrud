using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DvBCrud.Admin.Tests.Web.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<AdminDbContext>(options =>
{
    var connection = new SqliteConnection("Filename=:memory:");
    connection.Open();
    options.UseSqlite(connection);
}, ServiceLifetime.Singleton);

builder.Services.AddScoped<WeatherForecastMapper>();
builder.Services.AddScoped<WeatherForecastRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var context = app.Services.GetRequiredService<AdminDbContext>();
context.Database.EnsureCreated();
var forecasts = new[]
{
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse("2023-07-10T12:00:00+02:00"),
        TemperatureC = 25,
        Summary = "Sunny"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse("2023-07-10T18:00:00+02:00"),
        TemperatureC = 23,
        Summary = "Sunny"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse("2023-07-11T00:00:00+02:00"),
        TemperatureC = 17,
        Summary = "Clear"
    }
};
context.WeatherForecasts.AddRange(forecasts);
context.SaveChanges();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();