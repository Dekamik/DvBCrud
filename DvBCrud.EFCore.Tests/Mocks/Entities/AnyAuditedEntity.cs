using DvBCrud.EFCore.Entities;

namespace DvBCrud.EFCore.Tests.Mocks.Entities
{
    public class AnyAuditedEntity : BaseAuditedEntity<int, int>
    {
        public string AnyString { get; set; }

        protected override void CopyImpl(BaseEntity<int> other)
        {
            var o = other as AnyAuditedEntity;
            AnyString = o.AnyString;
        }
    }
}
