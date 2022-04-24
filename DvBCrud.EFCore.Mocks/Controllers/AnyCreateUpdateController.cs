using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.API.CrudActions;
using DvBCrud.EFCore.Mocks.Services;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    public class AnyCreateUpdateController : CrudController<string, AnyModel, IAnyService>
    {
        public AnyCreateUpdateController(IAnyService service) : base(service, CrudAction.Create, CrudAction.Update)
        {
        }
    }
}
