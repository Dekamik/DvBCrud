using DvBCrud.EFCore.API.Mocks.XMLJSON;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xunit;

namespace DvBCrud.EFCore.API.Tests.XMLJSON
{
    public class AsyncReadOnlyControllerTests
    {
        private readonly IAnyReadOnlyRepository repo;
        private readonly ILogger logger;
        private readonly IAnyAsyncReadOnlyController controller;

        public AsyncReadOnlyControllerTests()
        {
            repo = A.Fake<IAnyReadOnlyRepository>();
            logger = A.Fake<ILogger>();
            controller = new AnyAsyncReadOnlyController(repo, logger);
        }

        [Fact]
        public async Task Read_AnyId_ReturnsEntityFromRepository()
        {
            // Arrange
            var expected = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            A.CallTo(() => repo.GetAsync(1)).Returns(expected);

            // Act
            var result = (await controller.Read(1)).Result as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().Be(expected);
        }


        [Fact]
        public async Task Read_AnyNonExistingId_Returns404NotFound()
        {
            // Arrange
            A.CallTo(() => repo.GetAsync(1)).Returns(null as AnyEntity);

            // Act
            var result = (await controller.Read(1)).Result as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
            A.CallTo(() => repo.GetAsync(1)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ReadAll_Any_ReturnsAllEntitiesFromRepository()
        {
            // Arrange
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
            };
            A.CallTo(() => repo.GetAll()).Returns(expected);

            // Act
            var result = (await controller.ReadAll()).Result as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().Be(expected);
        }
    }
}
