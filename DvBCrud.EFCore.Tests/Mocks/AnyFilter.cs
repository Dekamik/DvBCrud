using DvBCrud.Shared.Filtering;

namespace DvBCrud.EFCore.Tests.Mocks;

public class AnyFilter : IPaginateFilter
{
    public enum AnyOrder
    {
        Id = 1,
        CreatedAt = 2,
        ModifiedAt = 3,
        AnyString = 4
    }

    public AnyOrder? Order { get; set; }
    public bool? Descending { get; set; }
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
    
    public string? AnyString { get; set; }
}