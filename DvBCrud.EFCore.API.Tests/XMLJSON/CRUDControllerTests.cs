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
        public void Create_AnyEntities_RepositoryCreatesEntities()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entities = new[]
            {
                new AnyEntity(),
                new AnyEntity()
            };

            // Act
            var result = controller.CreateRange(entities) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.CreateRange(entities)).MustHaveHappenedOnceExactly();
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
        public void Update_AnyEntities_RepositoryUpdatesEntities()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entities = new[]
            {
                new AnyEntity(),
                new AnyEntity()
            };

            // Act
            var result = controller.UpdateRange(entities) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.UpdateRange(entities, false)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Update_AnyEntitiesWithCreate_RepositoryUpdatesOrCreatesEntities()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entities = new[]
            {
                new AnyEntity(),
                new AnyEntity()
            };

            // Act
            var result = controller.UpdateRange(entities, true) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.UpdateRange(entities, true)).MustHaveHappenedOnceExactly();
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
        public void Delete_AnyEntitiesWithCreate_RepositoryDeletesOrCreatesEntities()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entities = new[] { 1, 2 };

            // Act
            var result = controller.DeleteRange(entities) as OkResult;

            // Assert
            result.Should().NotBeNull();
            A.CallTo(() => repo.DeleteRange(entities)).MustHaveHappenedOnceExactly();
        }
    }
}
