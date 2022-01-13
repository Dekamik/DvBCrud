using DvBCrud.EFCore.Entities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DvBCrud.EFCore.Mocks.Entities
{
    [ExcludeFromCodeCoverage]
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
