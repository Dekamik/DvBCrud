using DvBCrud.EFCore.Entities;

namespace DvBCrud.EFCore.Mocks.Entities
{
    public class AnyEntity : BaseEntity<int>
    {
        public string AnyString { get; set; }

        protected override void CopyImpl(BaseEntity<int> other)
        {
            var o = other as AnyEntity;
            AnyString = o.AnyString;
        }
    }
}
