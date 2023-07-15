namespace DvBCrud.API.Tests.Mocks;

public class AnyFilter
{
    public enum AnyOrder
    {
        None = 0,
        Id = 1,
        CreatedAt = 2,
        ModifiedAt = 3,
        AnyString = 4
    }

    public AnyOrder Order { get; set; }
    public bool Descending { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    
    public string? AnyString { get; set; }
}