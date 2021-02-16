using DvBCrud.EFCore.API.CRUDActions;
using DvBCrud.EFCore.API.XMLJSON;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Mocks.XMLJSON
{
    public class AnyCreateUpdateController : CRUDController<AnyEntity, int, AnyRepository, AnyDbContext>
    {
        public AnyCreateUpdateController(AnyRepository repository, ILogger logger) : base(repository, logger, CRUDAction.Create, CRUDAction.Update)
        {
        }
    }
}
