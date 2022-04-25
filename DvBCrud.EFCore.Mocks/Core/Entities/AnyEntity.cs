using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Entities;

namespace DvBCrud.EFCore.Mocks.Core.Entities
{
    [ExcludeFromCodeCoverage]
    public class AnyEntity : BaseEntity<string> // Using string type for Id to test nullable cases
    {
        public string AnyString { get; set; }

        protected override void CopyImpl(BaseEntity<string> other)
        {
            var o = other as AnyEntity;
            AnyString = o.AnyString;
        }
    }
}
