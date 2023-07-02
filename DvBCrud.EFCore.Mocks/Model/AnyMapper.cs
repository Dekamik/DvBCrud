using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Mocks.Core.Entities;

namespace DvBCrud.EFCore.Mocks.Services.Model;

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