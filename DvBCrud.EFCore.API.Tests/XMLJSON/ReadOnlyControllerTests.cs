using DvBCrud.EFCore.API.Mocks.XMLJSON;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DvBCrud.EFCore.API.Tests.XMLJSON
{
    public class ReadOnlyControllerTests
    {
        [Fact]
        public void Read_AnyId_ReturnsEntityFromRepository()
        {
            var repo = A.Fake<IAnyReadOnlyRepository>();
            var logger = A.Fake<ILogger>();
            var expected = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            A.CallTo(() => repo.Get(1)).Returns(expected);
            var controller = new AnyReadOnlyController(repo, logger);

            var result = controller.Read(1).Result as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().Be(expected);
        }

        [Fact]
        public void ReadRange_MultipleIds_ReturnsEntitiesFromRepository()
        {
            var repo = A.Fake<IAnyReadOnlyRepository>();
            var logger = A.Fake<ILogger>();
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
            var input = expected.Select(e => e.Id);
            A.CallTo(() => repo.GetRange(input)).Returns(expected);
            var controller = new AnyReadOnlyController(repo, logger);

            var result = controller.ReadRange(input).Result as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().Be(expected);
        }

        [Fact]
        public void ReadAll_Any_ReturnsAllEntitiesFromRepository()
        {
            var repo = A.Fake<IAnyReadOnlyRepository>();
            var logger = A.Fake<ILogger>();
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
            A.CallTo(() => repo.GetAll()).Returns(expected);
            var controller = new AnyReadOnlyController(repo, logger);

            var result = controller.ReadAll().Result as OkObjectResult;

            result.Should().NotBeNull();
            result.Value.Should().Be(expected);
        }
    }
}
