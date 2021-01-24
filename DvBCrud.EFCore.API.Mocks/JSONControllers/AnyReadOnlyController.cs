using DvBCrud.EFCore.API.JSON;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Mocks.JSONControllers
{
    public class AnyReadOnlyController : ReadOnlyController<AnyEntity, int, IAnyReadOnlyRepository, AnyDbContext>
    {
        public AnyReadOnlyController(IAnyReadOnlyRepository repository, ILogger logger) : base(repository, logger)
        {

        }
    }
}
