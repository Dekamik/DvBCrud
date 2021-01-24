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
    }
}
