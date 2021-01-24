using DvBCrud.EFCore.API.Mocks.JSONControllers;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xunit;

namespace DvBCrud.EFCore.API.Tests.JSONControllers
{
    public class CRUDControllerTests
    {
        [Fact]
        public async Task Create_AnyEntity_RepositoryCreatesEntity()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entity = new AnyEntity();

            // Act
            await controller.Create(new[] { entity });

            // Assert
            A.CallTo(() => repo.Create(entity)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Create_AnyEntities_RepositoryCreatesEntities()
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
            await controller.Create(entities);

            // Assert
            A.CallTo(() => repo.CreateRange(entities)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_AnyEntity_RepositoryUpdatesEntity()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entity = new AnyEntity();

            // Act
            await controller.Update(new[] { entity });

            // Assert
            A.CallTo(() => repo.Update(entity, false)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_AnyEntities_RepositoryUpdatesEntities()
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
            await controller.Update(entities);

            // Assert
            A.CallTo(() => repo.UpdateRange(entities, false)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_AnyEntityWithCreate_RepositoryUpdatesOrCreatesEntity()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entity = new AnyEntity();

            // Act
            await controller.Update(new[] { entity }, true);

            // Assert
            A.CallTo(() => repo.Update(entity, true)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_AnyEntitiesWithCreate_RepositoryUpdatesOrCreatesEntities()
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
            await controller.Update(entities, true);

            // Assert
            A.CallTo(() => repo.UpdateRange(entities, true)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_AnyEntityWithCreate_RepositoryDeletesOrCreatesEntity()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            int id = 1;

            // Act
            await controller.Delete(id);

            // Assert
            A.CallTo(() => repo.Delete(id)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_AnyEntitiesWithCreate_RepositoryDeletesOrCreatesEntities()
        {
            // Arrange
            var repo = A.Fake<IAnyRepository>();
            var logger = A.Fake<ILogger>();
            var controller = new AnyCRUDController(repo, logger);
            var entities = new[] { 1, 2 };

            // Act
            await controller.Delete(entities);

            // Assert
            A.CallTo(() => repo.DeleteRange(entities)).MustHaveHappenedOnceExactly();
        }
    }
}
