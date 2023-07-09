namespace DvBCrud.Shared.Entities;

/// <summary>
/// Interface for adding a ModifiedAt property to an <see cref="IEntity{TId}"/> for automatically persisting modification timestamp
/// </summary>
public interface IModifiedAt
{
    public DateTimeOffset ModifiedAt { get; set; }
}