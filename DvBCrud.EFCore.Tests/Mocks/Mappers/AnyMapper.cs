using System.Diagnostics.CodeAnalysis;

namespace DvBCrud.EFCore.Tests.Mocks.Mappers;

[ExcludeFromCodeCoverage]
public class AnyMapper : IAnyMapper
{
    public AnyModel ToModel(AnyEntity other)
    {
        return new AnyModel
        {
            Id = other.Id,
            AnyString = other.AnyString
        };
    }

    public AnyEntity ToEntity(AnyModel other)
    {
        return new AnyEntity
        {
            Id = other.Id,
            AnyString = other.AnyString
        };
    }

    public void UpdateEntity(AnyEntity target, AnyEntity other)
    {
        target.AnyString = other.AnyString;
    }
}