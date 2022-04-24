using DvBCrud.EFCore.Repositories;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data
{
    public interface IWeatherForecastRepository : IRepository<WeatherForecast, int>
    {
    }
}
