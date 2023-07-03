using System.Diagnostics.CodeAnalysis;

namespace DvBCrud.API.Tests.Mocks;

[ExcludeFromCodeCoverage]
public class AnyModel
{
    // Using string type for Id to test nullable cases
    public string Id { get; set; } = "";
    public string? AnyString { get; set; }
}