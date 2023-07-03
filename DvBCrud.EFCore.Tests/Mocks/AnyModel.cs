using System.Diagnostics.CodeAnalysis;

namespace DvBCrud.Mocks.Model;

[ExcludeFromCodeCoverage]
public class AnyModel
{
    // Using string type for Id to test nullable cases
    public string Id { get; set; } = "";
    public string? AnyString { get; set; }
}