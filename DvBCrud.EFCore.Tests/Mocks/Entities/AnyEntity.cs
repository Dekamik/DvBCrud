﻿using DvBCrud.EFCore.Entities;

namespace DvBCrud.EFCore.Tests.Mocks.Entities
{
    public class AnyEntity : BaseEntity<int>
    {
        public string AnyString { get; set; }

        protected override void CopyImpl(BaseEntity<int> other)
        {
            AnyEntity otherEntity = other as AnyEntity;
            AnyString = otherEntity.AnyString;
        }
    }
}
