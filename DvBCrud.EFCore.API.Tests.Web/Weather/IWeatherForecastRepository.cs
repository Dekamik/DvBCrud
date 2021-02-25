using DvBCrud.EFCore.Repositories;

namespace DvBCrud.EFCore.API.Tests.Web.Weather
{
    public interface IWeatherForecastRepository : IRepository<WeatherForecast, int>
    {
    }
}
