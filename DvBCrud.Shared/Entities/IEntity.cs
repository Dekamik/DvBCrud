namespace DvBCrud.Shared.Entities;

/// <summary>
/// Base interface for all entities that are to be manipulated by an IRepository
/// </summary>
/// <typeparam name="TId">Entity key type</typeparam>
public interface IEntity<TId>
{
    public TId Id { get; set; }
}