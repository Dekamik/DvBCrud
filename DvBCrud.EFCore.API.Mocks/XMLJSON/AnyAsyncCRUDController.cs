using DvBCrud.EFCore.API.XMLJSON;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Mocks.XMLJSON
{
    public class AnyAsyncCRUDController : AsyncCRUDController<AnyEntity, int, IAnyRepository, AnyDbContext>, IAnyAsyncCRUDController
    {
        public AnyAsyncCRUDController(IAnyRepository anyRepository, ILogger logger) : base(anyRepository, logger)
        {

        }
    }
}
