using DvBCrud.EFCore;

namespace DvBCrud.Admin.Tests.Web.Data;

public class WeatherForecastRepository : Repository<WeatherForecast, long, AdminDbContext, WeatherForecastMapper, WeatherForecastModel, WeatherForecastFilter>
{
    public WeatherForecastRepository(AdminDbContext context, WeatherForecastMapper mapper) : base(context, mapper)
    {
    }
}