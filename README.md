# DvBCrud

DvBCrud is a collection of CRUD templating libraries.

This project aims to both standardize and enable rapid development of normal database operations and use-cases.

## Projects

### [DvBCrud.EFCore](https://github.com/Dekamik/DvBCrud.EFCore)
[![Build Status](https://travis-ci.com/Dekamik/DvBCrud.EFCore.svg?branch=master)](https://travis-ci.com/Dekamik/DvBCrud.EFCore)
[![Nuget](https://img.shields.io/nuget/v/DvBCrud.EFCore?label=DvBCrud.EFCore)](https://www.nuget.org/packages/DvBCrud.EFCore/)
[![Nuget](https://img.shields.io/nuget/v/DvBCrud.EFCore.API?label=DvBCrud.EFCore.API)](https://www.nuget.org/packages/DvBCrud.EFCore.API/)

EFCore implementation.

### [DvBCrud.MongoDB](https://github.com/Dekamik/DvBCrud.MongoDB)
[![Build Status](https://travis-ci.com/Dekamik/DvBCrud.MongoDB.svg?branch=master)](https://travis-ci.com/Dekamik/DvBCrud.MongoDB)
[![Nuget](https://img.shields.io/nuget/v/DvBCrud.MongoDB?label=DvBCrud.MongoDB)](https://www.nuget.org/packages/DvBCrud.MongoDB/)
[![Nuget](https://img.shields.io/nuget/v/DvBCrud.MongoDB.API?label=DvBCrud.MongoDB.API)](https://www.nuget.org/packages/DvBCrud.MongoDB.API/)

WORK IN PROGRESS.

MongoDB implementation.

## How it works

The library consists of base classes that implement common REST and CRUD functionality.
The developer defines entities, repositories and controllers that inherit CRUD functionality from base classes.

These libraries have a unified structure across all projects.

## Example: Weather forecasts

This is a simplified example demonstrating a WeatherForecast endpoint that serves weather data using DvBCrud.EFCore:

1. Create entity

`WeatherForecast.cs`
```cs
public class WeatherForecast : BaseEntity<int>
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary { get; set; }

    protected override void CopyImpl(BaseEntity<int> other)
    {
        WeatherForecast o = other as WeatherForecast;
        Date = o.Date;
        TemperatureC = o.TemperatureC;
        Summary = o.Summary;
    }
}
```

2. Add it to the DbContext

`WebDbContext.cs`
```cs
public class WebDbContext : DbContext
{
    public WebDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}
```

3. Create the repository

`WeatherForecastRepository.cs`
```cs
public class WeatherForecastRepository : Repository<WeatherForecast, int, WebDbContext>
{
    public WeatherForecastRepository(WebDbContext dbContext, ILogger<WeatherForecastRepository> logger) : base(dbContext, logger)
    {
    }
}
```

4. Register the repository in `Startup.cs`

`Startup.ConfigureServices`
```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<WebDbContext>(options =>
    {
        {...}
    });

    services.AddTransient<WeatherForecastRepository>();

    services.AddControllers();
}
```

5. Create the controller

`WeatherForecastController.cs`
```cs
public class WeatherForecastController : CRUDController<WeatherForecast, int, WeatherForecastRepository, WebDbContext>
{
    public WeatherForecastController(WeatherForecastRepository repository, ILogger<WeatherForecastController> logger) : base(repository, logger)
    {
    }
}
```

This generates these REST endpoints for manipulating weather data:
* CREATE: `POST /weatherforecast/`
* READ: `GET /weatherforecast/{id}`
* READ ALL: `GET /weatherforecast/`
* UPDATE: `PUT /weatherforecast/{id}`
* DELETE: `DELETE /weatherforecast/{id}`

You can of-course extend both WeatherForecastRepository and WeatherForecastController with additional functionality.

## Example: Read-only endpoint

You want to make your data read-only? No problem. Simply define that only `CRUDAction.Read` is allowed in the overloaded constructor like below.

`WeatherForecastController.cs`
```cs
public class WeatherForecastController : CRUDController<WeatherForecast, int, WeatherForecastRepository, WebDbContext>
{
    public WeatherForecastController(WeatherForecastRepository repository, ILogger<WeatherForecastController> logger) : base(repository, logger, CRUDAction.Read)
    {
    }
}
```

The endpoint above will return a 403 FORBIDDEN response on requests for Create, Update and Delete actions.

## Example: Writable endpoint that disallow the Delete action

You can also define a selection of `CRUDAction`s to allow in the overloaded constructor like below.

`WeatherForecastController.cs`
```cs
public class WeatherForecastController : CRUDController<WeatherForecast, int, WeatherForecastRepository, WebDbContext>
{
    public WeatherForecastController(WeatherForecastRepository repository, ILogger<WeatherForecastController> logger) : base(repository, logger, CRUDAction.Create CRUDAction.Read, CRUDAction.Update)
    {
    }
}
```

The endpoint above will return a 403 FORBIDDEN response on requests for the Delete action only.
