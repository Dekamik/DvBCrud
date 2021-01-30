using DvBCrud.EFCore.API.XMLJSON;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Mocks.XMLJSON
{
    public class AnyCRUDController : CRUDController<AnyEntity, int, IAnyRepository, AnyDbContext>, IAnyCRUDController
    {
        public AnyCRUDController(IAnyRepository anyRepository, ILogger logger) : base(anyRepository, logger)
        {

        }
    }
}
