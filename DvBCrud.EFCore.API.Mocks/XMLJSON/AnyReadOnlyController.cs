using DvBCrud.EFCore.API.XMLJSON;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Mocks.XMLJSON
{
    public class AnyReadOnlyController : ReadOnlyController<AnyEntity, int, IAnyRepository, AnyDbContext>
    {
        public AnyReadOnlyController(IAnyRepository repository, ILogger logger) : base(repository, logger)
        {

        }
    }
}
