using DvBCrud.EFCore.API.Mocks.XMLJSON;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Xunit;

namespace DvBCrud.EFCore.API.Tests.XMLJSON
{
    public class ReadOnlyControllerTests
    {
        [Fact]
        public void Read_AnyId_ReturnsEntityFromRepository()
        {
            // Arrange
            var repo = A.Fake<IAnyReadOnlyRepository>();
            var logger = A.Fake<ILogger>();
            var expected = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            A.CallTo(() => repo.Get(1)).Returns(expected);
            var controller = new AnyReadOnlyController(repo, logger);

            // Act
            var result = controller.Read(1).Result as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().Be(expected);
        }


        [Fact]
        public void Read_AnyNonExistingId_Returns404NotFound()
        {
            // Arrange
            var repo = A.Fake<IAnyReadOnlyRepository>();
            var logger = A.Fake<ILogger>();
            A.CallTo(() => repo.Get(1)).Returns(null);
            var controller = new AnyReadOnlyController(repo, logger);

            // Act
            var result = controller.Read(1).Result as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
            A.CallTo(() => repo.Get(1)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void ReadAll_Any_ReturnsAllEntitiesFromRepository()
        {
            var repo = A.Fake<IAnyReadOnlyRepository>();
            var logger = A.Fake<ILogger>();
            var expected = new[]
            {
                new AnyEntity
                {
                    Id = 1,
                    AnyString = "AnyString"
                },
                new AnyEntity
                {
                    Id = 2,
                    AnyString = "AnyString"
                }
            }.AsEnumerable();
            A.CallTo(() => repo.GetAll()).Returns(expected);
            var controller = new AnyReadOnlyController(repo, logger);

            var result = controller.ReadAll().Result as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().Be(expected);
        }
    }
}
