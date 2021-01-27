using DvBCrud.EFCore.Mocks.Entities;
using FakeItEasy;
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
            // Arrange
            var original = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            var newEntity = new AnyEntity
            {
                AnyString = "AnyOtherString"
            };

            // Act
            newEntity.Copy(original);

            // Assert
            newEntity.AnyString.Should().Be(original.AnyString);
        }


        [Fact]
        public void Copy_WithNewId_IdIgnored()
        {
            // Arrange
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

            // Act
            newEntity.Copy(original);

            // Assert
            newEntity.Id.Should().Be(newEntity.Id);
            newEntity.AnyString.Should().Be(original.AnyString);
        }
    }
}
