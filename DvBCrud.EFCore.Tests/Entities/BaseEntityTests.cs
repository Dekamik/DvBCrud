using DvBCrud.EFCore.Mocks.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DvBCrud.EFCore.Tests.Entities
{
    public class BaseEntityTests
    {
        [Fact]
        public void Copy_AnyEntity_ValuesCopied()
        {
            var original = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            var newEntity = new AnyEntity
            {
                Id = 2,
                AnyString = "AnyOtherString"
            };

            newEntity.Copy(original);

            newEntity.Id.Should().NotBe(original.Id);
            newEntity.AnyString.Should().Be(original.AnyString);
        }
    }
}
