using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DvBCrud.EFCore.Entities
{
    /// <summary>
    /// Base type for all entities that are to be manipulated by IRepository and IReadOnlyRepository
    /// </summary>
    /// <typeparam name="TId">Entity key type</typeparam>
    public interface IEntity<TId>
    {
        public TId Id { get; set; }
    }
}
