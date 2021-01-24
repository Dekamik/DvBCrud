using System;

namespace DvBCrud.EFCore.Entities
{
    public abstract class BaseAuditedEntity<TId, TUserId> : BaseEntity<TId>
    {
        public DateTime CreatedAt { get; set; }

        public TUserId CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public TUserId UpdatedBy { get; set; }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="other"/> is null</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="other"/> isn't derived from <see cref="BaseAuditedEntity{TId, TUserId}"/></exception>
        public override void Copy(BaseEntity<TId> other)
        {
            if (other == null)
                throw new ArgumentNullException($"Argument {nameof(other)} cannot be null");

            if (!other.GetType().IsSubclassOf(typeof(BaseAuditedEntity<TId, TUserId>)))
                throw new ArgumentException($"Argument {nameof(other)} must derive from {typeof(BaseAuditedEntity<TId, TUserId>)}");

            var o = other as BaseAuditedEntity<TId, TUserId>;
            UpdatedAt = o.UpdatedAt;
            UpdatedBy = o.UpdatedBy;

            base.Copy(other);
        }

        protected abstract override void CopyImpl(BaseEntity<TId> other);
    }
}
