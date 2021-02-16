using DvBCrud.EFCore.API.Mocks.XMLJSON;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DvBCrud.EFCore.API.Tests.XMLJSON
{
    public class CRUDControllerTests
    {
        private readonly IAnyRepository repository;
        private readonly ILogger logger;
        private readonly IAnyCRUDController controller;

        public CRUDControllerTests()
        {
            repository = A.Fake<IAnyRepository>();
            logger = A.Fake<ILogger>();
            controller = new AnyCRUDController(repository, logger);
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
        public void Read_ReadForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyCreateUpdateController(repository, logger);
            int id = 1;

            // Act
            var result = restrictedController.Read(id).Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Get(id)).MustNotHaveHappened();
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

        [Fact]
        public void ReadAll_ReadForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyCreateUpdateController(repository, logger);

            // Act
            var result = restrictedController.ReadAll().Result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.GetAll()).MustNotHaveHappened();
        }

        [Fact]
        public void Create_AnyEntity_RepositoryCreatesEntity()
        {
            // Arrange
            var entity = new AnyEntity
            {
                AnyString = "AnyString"
            };

            // Act
            var result = controller.Create(entity) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repository.Create(entity)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Create_AnyEntityWithId_Returns400BadRequest()
        {
            // Arrange
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };

            // Act
            var result = controller.Create(entity) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            A.CallTo(() => repository.Create(entity)).MustNotHaveHappened();
        }

        [Fact]
        public void Create_CreateForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyReadOnlyController(repository, logger);
            var entity = new AnyEntity();

            // Act
            var result = restrictedController.Create(entity) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Create(entity)).MustNotHaveHappened();
        }

        [Fact]
        public void Update_AnyEntity_RepositoryUpdatesEntity()
        {
            // Arrange
            var entity = new AnyEntity 
            {
                AnyString = "AnyString"
            };

            // Act
            var result = controller.Update(1, entity) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repository.Update(1, entity)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Update_AnyNonExistingEntity_Returns404NotFound()
        {
            // Arrange
            var entity = new AnyEntity
            {
                AnyString = "AnyString"
            };
            A.CallTo(() => repository.Update(1, entity)).Throws<KeyNotFoundException>();

            // Act
            var result = controller.Update(1, entity) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public void Update_UpdateForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyReadOnlyController(repository, logger);
            int id = 1;
            var entity = new AnyEntity();

            // Act
            var result = restrictedController.Update(id, entity) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Update(id, entity)).MustNotHaveHappened();
        }

        [Fact]
        public void Delete_AnyEntityWithCreate_RepositoryDeletesOrCreatesEntity()
        {
            // Arrange
            int id = 1;

            // Act
            var result = controller.Delete(id) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repository.Delete(id)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void Delete_NonExistingEntity_Returns404NotFound()
        {
            // Arrange
            A.CallTo(() => repository.Delete(1)).Throws<KeyNotFoundException>();

            // Act
            var result = controller.Delete(1) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public void Delete_DeleteForbidden_ReturnsForbidden()
        {
            // Arrange
            var restrictedController = new AnyReadOnlyController(repository, logger);
            int id = 1;

            // Act
            var result = restrictedController.Delete(id) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(403);
            A.CallTo(() => repository.Delete(id)).MustNotHaveHappened();
        }
    }
}
