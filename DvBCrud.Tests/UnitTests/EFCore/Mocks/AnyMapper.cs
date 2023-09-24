using System.Diagnostics.CodeAnalysis;
using DvBCrud.Shared;

namespace DvBCrud.Tests.UnitTests.EFCore.Mocks;

[ExcludeFromCodeCoverage]
public class AnyMapper : BaseMapper<AnyEntity, AnyModel, AnyFilter>
{
    public override AnyModel ToModel(AnyEntity other)
    {
        return new AnyModel
        {
            Id = other.Id,
            AnyString = other.AnyString
        };
    }

    public override AnyEntity ToEntity(AnyModel other)
    {
        return new AnyEntity
        {
            Id = other.Id,
            AnyString = other.AnyString
        };
    }

    public override void UpdateEntity(AnyEntity target, AnyEntity other)
    {
        target.AnyString = other.AnyString;
    }

    public override IEnumerable<AnyEntity> FilterAndSort(IEnumerable<AnyEntity> entities, AnyFilter filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.AnyString))
            entities = entities.Where(e => e.AnyString?.Contains(filter.AnyString) ?? false);

        switch (filter.Order)
        {
            case AnyFilter.AnyOrder.Id:
                entities = entities.OrderBy(e => e.Id);
                break;
            case AnyFilter.AnyOrder.CreatedAt:
                entities = entities.OrderBy(e => e.CreatedAt);
                break;
            case AnyFilter.AnyOrder.ModifiedAt:
                entities = entities.OrderBy(e => e.ModifiedAt);
                break;
            case AnyFilter.AnyOrder.AnyString:
                entities = entities.OrderBy(e => e.AnyString);
                break;
            case null:
                break;
            default:
#pragma warning disable CA2208
                throw new ArgumentOutOfRangeException($"{nameof(filter)}.{nameof(filter.Order)}");
#pragma warning restore CA2208
        }

        if (filter.Descending.HasValue && filter.Descending.Value)
            entities = entities.Reverse();

        return base.FilterAndSort(entities, filter);
    }
}