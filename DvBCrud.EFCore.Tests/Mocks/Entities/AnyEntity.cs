using DvBCrud.EFCore.Entities;

namespace DvBCrud.EFCore.Tests.Mocks.Entities
{
    public class AnyEntity : BaseEntity<int>
    {
        public string AnyString { get; set; }

        public override void Copy(BaseEntity<int> other)
        {
            AnyEntity otherEntity = other as AnyEntity;
            AnyString = otherEntity.AnyString;
        }
    }
}
