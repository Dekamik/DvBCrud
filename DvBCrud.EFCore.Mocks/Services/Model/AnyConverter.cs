using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Mocks.Core.Entities;
using DvBCrud.EFCore.Services.Models;

namespace DvBCrud.EFCore.Mocks.Services.Model;

[ExcludeFromCodeCoverage]
public class AnyConverter : Converter<AnyEntity, AnyModel>, IAnyConverter
{
    public override AnyModel ToModel(AnyEntity entity) =>
        new()
        {
            Id = entity.Id,
            AnyString = entity.AnyString
        };

    public override AnyEntity ToEntity(AnyModel model) =>
        new()
        {
            Id = model.Id,
            AnyString = model.AnyString
        };
}