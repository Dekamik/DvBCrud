using System;

namespace DvBCrud.EFCore.Entities;

/// <summary>
/// Interface for adding a CreatedAt property to an <see cref="IEntity{TId}"/> for automatically persisting creation timestamp
/// </summary>
public interface ICreatedAt
{
    public DateTimeOffset CreatedAt { get; set; }
}