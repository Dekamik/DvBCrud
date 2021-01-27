using DvBCrud.EFCore.API.Mocks.XMLJSON;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            var entity = new AnyEntity();

            // Act
            var result = controller.Create(entity) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.Create(entity)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Update_AnyEntity_RepositoryUpdatesEntity()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entity = new AnyEntity();

            // Act
            var result = controller.Update(entity) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.Update(entity, false)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Update_AnyEntityWithCreate_RepositoryUpdatesOrCreatesEntity()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entity = new AnyEntity();

            // Act
            var result = controller.Update(entity, true) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.Update(entity, true)).MustHaveHappenedOnceExactly();
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
    }
}
