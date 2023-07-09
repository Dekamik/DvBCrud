using System.Diagnostics.CodeAnalysis;

namespace DvBCrud.EFCore.Tests.Mocks.Mappers;

[ExcludeFromCodeCoverage]
public class AnyMapper : IAnyMapper
{
    public AnyModel ToModel(AnyEntity entity)
    {
        return new AnyModel
        {
            Id = entity.Id,
            AnyString = entity.AnyString
        };
    }

    public AnyEntity ToEntity(AnyModel model)
    {
        return new AnyEntity
        {
            Id = model.Id,
            AnyString = model.AnyString
        };
    }

    public void UpdateEntity(AnyEntity destination, AnyEntity source)
    {
        destination.AnyString = source.AnyString;
    }
}