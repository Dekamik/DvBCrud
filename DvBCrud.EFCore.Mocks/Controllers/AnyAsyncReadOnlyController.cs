using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.Mocks.Services;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    public class AnyAsyncReadOnlyController : AsyncCrudController<string, AnyModel, IAnyService>
    {
        public AnyAsyncReadOnlyController(IAnyService service) : base(service, CrudAction.Read)
        {
        }
    }
}
