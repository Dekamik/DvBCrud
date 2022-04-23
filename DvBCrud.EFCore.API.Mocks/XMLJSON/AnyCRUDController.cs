using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Mocks.XMLJSON
{
    [ExcludeFromCodeCoverage]
    public class AnyCRUDController : CrudController<AnyEntity, int, IAnyRepository>
    {
        public AnyCRUDController(IAnyRepository anyRepository, ILogger logger) : base(anyRepository)
        {

        }
    }
}
