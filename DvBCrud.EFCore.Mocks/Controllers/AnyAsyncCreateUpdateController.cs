using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.Mocks.Services;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    [AllowedActions(CrudAction.Create, CrudAction.Update)]
    public class AnyAsyncCreateUpdateController : AsyncCrudController<string, AnyModel, IAnyCrudHandler>
    {
        public AnyAsyncCreateUpdateController(IAnyCrudHandler crudHandler) : base(crudHandler)
        {
        }
    }
}
