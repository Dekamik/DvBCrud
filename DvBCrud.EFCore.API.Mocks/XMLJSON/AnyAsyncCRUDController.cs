using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.XMLJSON;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Mocks.XMLJSON
{
    [ExcludeFromCodeCoverage]
    public class AnyAsyncCRUDController : AsyncCRUDController<AnyEntity, int, IAnyRepository, AnyDbContext>
    {
        public AnyAsyncCRUDController(IAnyRepository anyRepository, ILogger logger) : base(anyRepository, logger)
        {

        }
    }
}
