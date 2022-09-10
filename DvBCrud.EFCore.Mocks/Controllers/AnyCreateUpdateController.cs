using System.Diagnostics.CodeAnalysis;
using DvBCrud.Common.Api.CrudActions;
using DvBCrud.EFCore.API.Controllers;
using DvBCrud.EFCore.Mocks.Services;
using DvBCrud.EFCore.Mocks.Services.Model;

namespace DvBCrud.EFCore.Mocks.Controllers
{
    [ExcludeFromCodeCoverage]
    [AllowedActions(CrudAction.Create, CrudAction.Update)]
    public class AnyCreateUpdateController : CrudController<string, AnyModel, IAnyService>
    {
        public AnyCreateUpdateController(IAnyService service) : base(service)
        {
        }
    }
}
