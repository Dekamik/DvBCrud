using System;

namespace DvBCrud.EFCore.Entities
{
    public abstract class BaseAuditedEntity<TId, TUserId> : BaseEntity<TId>
    {
        public DateTime CreatedAt { get; set; }

        public TUserId CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public TUserId UpdatedBy { get; set; }

        public override void Copy(BaseEntity<TId> other)
        {
            var o = other as BaseAuditedEntity<TId, TUserId>;
            CreatedAt = o.CreatedAt;
            CreatedBy = o.CreatedBy;
            UpdatedAt = o.UpdatedAt;
            UpdatedBy = o.UpdatedBy;

            base.Copy(other);
        }

        protected abstract override void CopyImpl(BaseEntity<TId> other);
    }
}
