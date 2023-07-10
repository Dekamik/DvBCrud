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

var currentDate = DateTimeOffset.Now;
var todayStr = currentDate.ToString("yyyy-MM-dd");
var tomorrowStr = currentDate.AddDays(1).ToString("yyyy-MM-dd");
var in2Days = currentDate.AddDays(2).ToString("yyyy-MM-dd");
var in3Days = currentDate.AddDays(3).ToString("yyyy-MM-dd");

var forecasts = new[]
{
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{todayStr}T12:00:00+02:00"),
        TemperatureC = 25,
        Summary = "Sunny"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{todayStr}T18:00:00+02:00"),
        TemperatureC = 23,
        Summary = "Sunny"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{tomorrowStr}T00:00:00+02:00"),
        TemperatureC = 17,
        Summary = "Clear"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{tomorrowStr}T06:00:00+02:00"),
        TemperatureC = 16,
        Summary = "Sunny"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{tomorrowStr}T12:00:00+02:00"),
        TemperatureC = 23,
        Summary = "Sunny"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{tomorrowStr}T18:00:00+02:00"),
        TemperatureC = 23,
        Summary = "Cloudy"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{in2Days}T00:00:00+02:00"),
        TemperatureC = 18,
        Summary = "Cloudy"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{in2Days}T06:00:00+02:00"),
        TemperatureC = 17,
        Summary = "Cloudy"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{in2Days}T12:00:00+02:00"),
        TemperatureC = 23,
        Summary = "Overcast"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{in2Days}T18:00:00+02:00"),
        TemperatureC = 20,
        Summary = "Rain"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{in3Days}T00:00:00+02:00"),
        TemperatureC = 16,
        Summary = "Overcast"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{in3Days}T06:00:00+02:00"),
        TemperatureC = 17,
        Summary = "Cloudy"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{in3Days}T12:00:00+02:00"),
        TemperatureC = 22,
        Summary = "Sunny"
    },
    new WeatherForecast
    {
        Date = DateTimeOffset.Parse($"{in3Days}T18:00:00+02:00"),
        TemperatureC = 21,
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