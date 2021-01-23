using DvBCrud.EFCore.Entities;

namespace DvBCrud.EFCore.Tests.Mocks.Entities
{
    public class AnyNullableIdEntity : BaseEntity<string>
    {
        public string AnyString { get; set; }

        protected override void CopyImpl(BaseEntity<string> other)
        {
            AnyNullableIdEntity otherEntity = other as AnyNullableIdEntity;
            AnyString = otherEntity.AnyString;
        }
    }
}
