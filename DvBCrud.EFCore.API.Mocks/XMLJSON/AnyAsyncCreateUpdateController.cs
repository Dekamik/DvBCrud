using DvBCrud.EFCore.API.CRUDActions;
using DvBCrud.EFCore.API.XMLJSON;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Mocks.XMLJSON
{
    public class AnyAsyncCreateUpdateController : AsyncCRUDController<AnyEntity, int, IAnyRepository, AnyDbContext>, IAnyAsyncCreateUpdateController
    {
        public AnyAsyncCreateUpdateController(IAnyRepository repository, ILogger logger) : base(repository, logger, CRUDAction.Create, CRUDAction.Update)
        {
        }
    }
}
