﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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

    public IEnumerable<AnyEntity> FilterOrderAndPaginate(IEnumerable<AnyEntity> entities, AnyFilter filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.AnyString))
        {
            entities = entities.Where(e => e.AnyString?.Contains(filter.AnyString) ?? false);
        }
        
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

        return entities.Skip(filter.Skip)
            .Take(filter.Take);
    }
}