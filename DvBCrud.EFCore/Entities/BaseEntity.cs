using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DvBCrud.EFCore.Entities
{
    /// <summary>
    /// Base type for all entities that are to be manipulated by IRepository and IReadOnlyRepository
    /// </summary>
    /// <typeparam name="TId">Entity key type</typeparam>
    public abstract class BaseEntity<TId>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TId Id { get; set; }

        /// <summary>
        /// Copy other entity's values to this entity
        /// </summary>
        /// <param name="other">Other <see cref="BaseEntity{TId}"/> to copy values from</param>
        public virtual void Copy(BaseEntity<TId> other)
        {
            CopyImpl(other);
        }

        /// <summary>
        /// Method that implements derived classes copy functionality.
        /// Gets called by <see cref="Copy(BaseEntity{TId})"/>
        /// </summary>
        /// <param name="other">Other <see cref="BaseEntity{TId}"/> to copy values from</param>
        protected abstract void CopyImpl(BaseEntity<TId> other);
    }
}
