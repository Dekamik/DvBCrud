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
        private readonly IAnyRepository repository;
        private readonly ILogger logger;
        private readonly AnyAsyncCRUDController controller;

        public AsyncCRUDControllerTests()
        {
            repository = A.Fake<IAnyRepository>();
            logger = A.Fake<ILogger>();
            controller = new AnyAsyncCRUDController(repository, logger);
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
            A.CallTo(() => repository.GetAsync(1)).Returns(expected);

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
            A.CallTo(() => repository.GetAsync(1)).Returns(null as AnyEntity);

            // Act
            var result = (await controller.Read(1)).Result as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
            A.CallTo(() => repository.GetAsync(1)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Read_ReadForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyAsyncCreateUpdateController(repository, logger);
            int id = 1;

            // Act
            var result = (await restrictedController.Read(id)).Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.GetAsync(id)).MustNotHaveHappened();
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
            A.CallTo(() => repository.GetAll()).Returns(expected);

            // Act
            var result = (await controller.ReadAll()).Result as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().Be(expected);
        }

        [Fact]
        public async Task ReadAll_ReadForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyAsyncCreateUpdateController(repository, logger);

            // Act
            var result = (await restrictedController.ReadAll()).Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.GetAll()).MustNotHaveHappened();
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
            A.CallTo(() => repository.Create(entity)).MustHaveHappenedOnceExactly();
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
            A.CallTo(() => repository.Create(entity)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Create_CreateForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyAsyncReadOnlyController(repository, logger);
            var entity = new AnyEntity();

            // Act
            var result = await restrictedController.Create(entity) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Create(entity)).MustNotHaveHappened();
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
            A.CallTo(() => repository.UpdateAsync(1, entity)).MustHaveHappenedOnceExactly();
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
            A.CallTo(() => repository.UpdateAsync(1, entity)).MustNotHaveHappened();
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
            A.CallTo(() => repository.UpdateAsync(1, entity)).Throws<KeyNotFoundException>();

            // Act
            var result = await controller.Update(1, entity) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Update_UpdateForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyAsyncReadOnlyController(repository, logger);
            int id = 1;
            var entity = new AnyEntity();

            // Act
            var result = await restrictedController.Update(id, entity) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Update(id, entity)).MustNotHaveHappened();
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
            A.CallTo(() => repository.Delete(id)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_NonExistingEntity_Returns404NotFound()
        {
            // Arrange
            A.CallTo(() => repository.Delete(1)).Throws<KeyNotFoundException>();

            // Act
            var result = await controller.Delete(1) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Delete_DeleteForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyAsyncReadOnlyController(repository, logger);
            int id = 1;

            // Act
            var result = await restrictedController.Delete(id) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Delete(id)).MustNotHaveHappened();
        }
    }
}
