using DvBCrud.EFCore.API.CRUDActions;
using DvBCrud.EFCore.API.XMLJSON;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Mocks.XMLJSON
{
    public class AnyAsyncReadOnlyController : AsyncCRUDController<AnyEntity, int, IAnyRepository, AnyDbContext>
    {
        public AnyAsyncReadOnlyController(IAnyRepository repository, ILogger logger) : base(repository, logger, CRUDAction.Read)
        {
        }
    }
}
