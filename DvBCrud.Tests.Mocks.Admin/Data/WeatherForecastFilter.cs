namespace DvBCrud.Admin.Tests.Web.Data;

public class WeatherForecastFilter
{
    public enum OrderBy
    {
        Date = 1,
        Temperature = 2,
        Summary = 3,
    }

    public OrderBy? Order { get; set; }
    public bool? Descending { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }

    public DateTime? Date { get; set; }
    public string? Summary { get; set; }
}