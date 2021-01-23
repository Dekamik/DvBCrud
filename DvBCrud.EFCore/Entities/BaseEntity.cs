using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DvBCrud.EFCore.Entities
{
    public abstract class BaseEntity<TId>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TId Id { get; set; }

        /// <summary>
        /// Copy other entity's values to this entity
        /// </summary>
        /// <param name="other">Other entity to copy from</param>
        public abstract void Copy(BaseEntity<TId> other);
    }
}
