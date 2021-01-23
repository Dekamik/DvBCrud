using System;

namespace DvBCrud.EFCore.Entities
{
    public abstract class BaseAuditedEntity<TId, TUserId> : BaseEntity<TId>
    {
        public DateTime CreatedAt { get; set; }

        public TUserId CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public TUserId UpdatedBy { get; set; }
    }
}
