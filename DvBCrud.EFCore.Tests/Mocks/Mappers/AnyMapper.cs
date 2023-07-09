using System.Diagnostics.CodeAnalysis;

namespace DvBCrud.EFCore.Tests.Mocks.Mappers;

[ExcludeFromCodeCoverage]
public class AnyMapper : IAnyMapper
{
    public AnyModel ToModel(AnyEntity entity) =>
        new()
        {
            Id = entity.Id,
            AnyString = entity.AnyString
        };

    public AnyEntity ToEntity(AnyModel model) =>
        new()
        {
            Id = model.Id,
            AnyString = model.AnyString
        };

    public void UpdateEntity(AnyEntity source, AnyEntity destination)
    {
        destination.AnyString = source.AnyString;
    }
}