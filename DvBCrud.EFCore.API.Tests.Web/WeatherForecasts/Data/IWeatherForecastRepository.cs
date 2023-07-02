using DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Model;
using DvBCrud.EFCore.Repositories;

namespace DvBCrud.EFCore.API.Tests.Web.WeatherForecasts.Data
{
    public interface IWeatherForecastRepository : IRepository<int, WeatherForecastModel>
    {
    }
}
