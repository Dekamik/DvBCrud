namespace DvBCrud.Tests.UnitTests.EFCore.Mocks;

public class AnyModel
{
    // Using string type for Id to test nullable cases
    public string Id { get; set; } = "";
    public string? AnyString { get; set; }
}