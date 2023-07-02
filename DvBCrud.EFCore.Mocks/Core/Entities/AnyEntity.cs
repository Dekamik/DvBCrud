using System;
using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Entities;

namespace DvBCrud.EFCore.Mocks.Core.Entities
{
    [ExcludeFromCodeCoverage]
    public class AnyEntity : BaseEntity<string>, ICreatedAt, IModifiedAt // Using string type for Id to test nullable cases
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        
        public string? AnyString { get; set; }

        protected override void CopyImpl(BaseEntity<string> other)
        {
            var o = other as AnyEntity;
            AnyString = o?.AnyString;
        }
    }
}
