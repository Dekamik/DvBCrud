using DvBCrud.EFCore.API.Mocks.XMLJSON;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Xunit;

namespace DvBCrud.EFCore.API.Tests.XMLJSON
{
    public class CRUDControllerTests
    {
        [Fact]
        public void Create_AnyEntity_RepositoryCreatesEntity()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entity = new AnyEntity
            {
                AnyString = "AnyString"
            };

            // Act
            var result = controller.Create(entity) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.Create(entity)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Create_AnyEntityWithId_Returns400BadRequest()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
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
            A.CallTo(() => repo.Create(entity)).MustNotHaveHappened();
        }

        [Fact]
        public void Update_AnyEntity_RepositoryUpdatesEntity()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entity = new AnyEntity {
                Id = 1,
                AnyString = "AnyString"
            };

            // Act
            var result = controller.Update(entity) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.Update(entity)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Update_AnyEntityWithoutId_Returns400BadRequest()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entity = new AnyEntity
            {
                AnyString = "AnyString"
            };

            // Act
            var result = controller.Update(entity) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            A.CallTo(() => repo.Update(entity)).MustNotHaveHappened();
        }


        [Fact]
        public void Update_AnyNonExistingEntity_Returns404NotFound()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            A.CallTo(() => repo.Update(entity)).Throws<KeyNotFoundException>();

            // Act
            var result = controller.Update(entity) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public void Delete_AnyEntityWithCreate_RepositoryDeletesOrCreatesEntity()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            int id = 1;

            // Act
            var result = controller.Delete(id) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.Delete(id)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public void Delete_NonExistingEntity_Returns404NotFound()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            A.CallTo(() => repo.Delete(1)).Throws<KeyNotFoundException>();

            // Act
            var result = controller.Delete(1) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }
    }
}
