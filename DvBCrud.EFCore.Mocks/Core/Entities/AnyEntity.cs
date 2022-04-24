using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Entities;

namespace DvBCrud.EFCore.Mocks.Core.Entities
{
    // Using string type for Id to test nullable cases
    [ExcludeFromCodeCoverage]
    public class AnyEntity : BaseEntity<string>
    {
        public string AnyString { get; set; }

        protected override void CopyImpl(BaseEntity<string> other)
        {
            var o = other as AnyEntity;
            AnyString = o.AnyString;
        }
    }
}
