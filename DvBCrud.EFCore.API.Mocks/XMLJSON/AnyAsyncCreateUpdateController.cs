using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using Microsoft.Extensions.Logging;

namespace DvBCrud.EFCore.API.Mocks.XMLJSON
{
    [ExcludeFromCodeCoverage]
    public class AnyAsyncCreateUpdateController : AsyncCrudController<AnyEntity, int, IAnyRepository>
    {
        public AnyAsyncCreateUpdateController(IAnyRepository repository, ILogger logger) : base(repository, logger, CrudAction.Create, CrudAction.Update)
        {
        }
    }
}
