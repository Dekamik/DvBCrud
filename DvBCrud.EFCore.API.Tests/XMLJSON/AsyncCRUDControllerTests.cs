using DvBCrud.EFCore.API.Mocks.XMLJSON;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DvBCrud.EFCore.API.Tests.XMLJSON
{
    public class AsyncCRUDControllerTests
    {
        private readonly IAnyRepository repo;
        private readonly ILogger logger;
        private readonly IAnyAsyncCRUDController controller;

        public AsyncCRUDControllerTests()
        {
            repo = A.Fake<IAnyRepository>();
            logger = A.Fake<ILogger>();
            controller = new AnyAsyncCRUDController(repo, logger);
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

        [Fact]
        public async Task Create_AnyEntity_RepositoryCreatesEntity()
        {
            // Arrange
            var entity = new AnyEntity 
            {
                AnyString = "AnyString"
            };

            // Act
            var result = await controller.Create(entity) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.Create(entity)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Create_AnyEntityWithId_Returns400BadRequest()
        {
            // Arrange
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };

            // Act
            var result = await controller.Create(entity) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            A.CallTo(() => repo.Create(entity)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Update_AnyEntity_RepositoryUpdatesEntity()
        {
            // Arrange
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };

            // Act
            var result = await controller.Update(1, entity) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.UpdateAsync(1, entity)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_AnyEntityWithoutId_Returns400BadRequest()
        {
            // Arrange
            var entity = new AnyEntity 
            { 
                AnyString = "AnyString"
            };

            // Act
            var result = await controller.Update(1, entity) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.UpdateAsync(1, entity)).MustNotHaveHappened();
        }


        [Fact]
        public async Task Update_AnyNonExistingEntity_Returns404NotFound()
        {
            // Arrange
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            A.CallTo(() => repo.UpdateAsync(1, entity)).Throws<KeyNotFoundException>();

            // Act
            var result = await controller.Update(1, entity) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Delete_AnyEntityWithCreate_RepositoryDeletesOrCreatesEntity()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await controller.Delete(id) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.Delete(id)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_NonExistingEntity_Returns404NotFound()
        {
            // Arrange
            A.CallTo(() => repo.Delete(1)).Throws<KeyNotFoundException>();

            // Act
            var result = await controller.Delete(1) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }
    }
}
