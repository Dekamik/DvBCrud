using DvBCrud.EFCore.API.JSON;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Mocks.JSONControllers
{
    public class AnyCRUDController : CRUDController<AnyEntity, int, IAnyRepository, AnyDbContext>
    {
        public AnyCRUDController(IAnyRepository anyRepository, ILogger logger) : base(anyRepository, logger)
        {

        }
    }
}
