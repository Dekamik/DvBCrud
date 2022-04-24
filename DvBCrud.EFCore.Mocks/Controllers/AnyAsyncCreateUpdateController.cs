using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.Mocks.Services;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    public class AnyAsyncCreateUpdateController : AsyncCrudController<string, AnyModel, IAnyService>
    {
        public AnyAsyncCreateUpdateController(IAnyService service) : base(service, CrudAction.Create, CrudAction.Update)
        {
        }
    }
}
