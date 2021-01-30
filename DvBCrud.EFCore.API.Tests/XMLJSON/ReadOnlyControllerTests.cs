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
        private readonly ILogger logger;
        private readonly IAnyRepository repository;
        private readonly IAnyReadOnlyController controller;

        public ReadOnlyControllerTests()
        {
            repository = A.Fake<IAnyRepository>();
            logger = A.Fake<ILogger>();
            controller = new AnyReadOnlyController(repository, logger);
        }

        [Fact]
        public void Read_AnyId_ReturnsEntityFromRepository()
        {
            // Arrange
            var expected = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            A.CallTo(() => repository.Get(1)).Returns(expected);

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
            A.CallTo(() => repository.Get(1)).Returns(null);

            // Act
            var result = controller.Read(1).Result as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
            A.CallTo(() => repository.Get(1)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void ReadAll_Any_ReturnsAllEntitiesFromRepository()
        {
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
            A.CallTo(() => repository.GetAll()).Returns(expected);

            var result = controller.ReadAll().Result as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().Be(expected);
        }
    }
}
