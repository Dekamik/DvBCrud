using System.Diagnostics.CodeAnalysis;
using DvBCrud.Common.Api.CrudActions;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.Mocks.Services;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    [AllowedActions(CrudAction.Read)]
    public class AnyAsyncReadOnlyController : AsyncCrudController<string, AnyModel, IAnyService>
    {
        public AnyAsyncReadOnlyController(IAnyService service) : base(service)
        {
        }
    }
}
