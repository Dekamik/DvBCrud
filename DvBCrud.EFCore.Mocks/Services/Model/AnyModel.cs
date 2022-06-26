using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Services.Models;

namespace DvBCrud.EFCore.Mocks.Services.Model;

[ExcludeFromCodeCoverage]
public class AnyModel : BaseModel
{
    // Using string type for Id to test nullable cases
    public string Id { get; set; }
    public string AnyString { get; set; }
}