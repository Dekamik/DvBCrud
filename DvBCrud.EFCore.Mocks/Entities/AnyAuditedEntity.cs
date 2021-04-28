﻿using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Entities;

namespace DvBCrud.EFCore.Mocks.Entities
{
    [ExcludeFromCodeCoverage]
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
